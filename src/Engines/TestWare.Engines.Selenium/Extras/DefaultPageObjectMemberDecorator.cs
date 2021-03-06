#nullable enable

using System.Collections.ObjectModel;
using System.Reflection;
using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras.MemberBuilders;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Default decorator determining how members of a class which represent elements in a Page Object
/// are detected.
/// </summary>
public class DefaultPageObjectMemberDecorator : IPageObjectMemberDecorator
{
    private static readonly List<IMemberBuilder> _memberBuilders = new List<IMemberBuilder> {
            new WebElementBuilder(),
            new WebElementListBuilder(),
            new WrappedElementBuilder(),
            new WrappedElementListBuilder()
        };

    /// <summary>
    /// Locates an element or list of elements for a Page Object member, and returns a
    /// proxy object for the element or list of elements.
    /// </summary>
    /// <param name="member">The <see cref="MemberInfo"/> containing information about
    /// a class's member.</param>
    /// <param name="locator">The <see cref="IElementLocator"/> used to locate elements.</param>
    /// <returns>A transparent proxy to the WebDriver element object.</returns>
    public virtual object? Decorate(MemberInfo member, IElementLocator locator)
    {
        FieldInfo? field = member as FieldInfo;
        PropertyInfo? property = member as PropertyInfo;

        Type? targetType = null;
        if (field != null)
        {
            targetType = field.FieldType;
        }

        if (property != null && property.CanWrite)
        {
            targetType = property.PropertyType;
        }

        if (targetType == null)
        {
            return null;
        }

        IList<By> bys = CreateLocatorList(member);
        if (bys.Count > 0)
        {
            bool cache = ShouldCacheLookup(member);
            return CreateObject(targetType, locator, bys, cache);
        }

        return null;
    }

    public virtual object CreateObject(Type memberType, IElementLocator locator, IEnumerable<By> bys, bool cache)
    {
        foreach (var builder in _memberBuilders)
        {
            if (builder.CreateObject(memberType, locator, bys, cache, out object? createdObject))
            {
                return createdObject;
            }
        }

        throw new ArgumentException($"Type of member '{memberType.Name}' is not IWebElement or IList<IWebElement>");
    }

    /// <summary>
    /// Determines whether lookups on this member should be cached.
    /// </summary>
    /// <param name="member">The <see cref="MemberInfo"/> containing information about
    /// the member of the Page Object class.</param>
    /// <returns><see langword="true"/> if lookups are to be cached; otherwise, <see langword="false"/>.</returns>
    protected static bool ShouldCacheLookup(MemberInfo member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member), "member cannot be null");
        }

        var cacheAttributeType = typeof(CacheLookupAttribute);
        bool cache = member.GetCustomAttributes(cacheAttributeType, true).Length > 0
            || member.DeclaringType?.GetCustomAttributes(cacheAttributeType, true).Length > 0;

        return cache;
    }

    /// <summary>
    /// Creates a list of <see cref="By"/> locators based on the attributes of this member.
    /// </summary>
    /// <param name="member">The <see cref="MemberInfo"/> containing information about
    /// the member of the Page Object class.</param>
    /// <returns>A list of <see cref="By"/> locators based on the attributes of this member.</returns>
    protected static ReadOnlyCollection<By> CreateLocatorList(MemberInfo member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member), "member cannot be null");
        }

        var useSequenceAttributes = Attribute.GetCustomAttributes(member, typeof(FindsBySequenceAttribute), true);
        bool useSequence = useSequenceAttributes.Length > 0;

        var useFindAllAttributes = Attribute.GetCustomAttributes(member, typeof(FindsByAllAttribute), true);
        bool useAll = useFindAllAttributes.Length > 0;

        if (useSequence && useAll)
        {
            throw new ArgumentException("Cannot specify FindsBySequence and FindsByAll on the same member");
        }

        List<By> bys = new List<By>();
        var attributes = Attribute.GetCustomAttributes(member, typeof(AbstractFindsByAttribute), true);
        if (attributes.Length > 0)
        {
            Array.Sort(attributes);
            foreach (var attribute in attributes)
            {
                var castedAttribute = (AbstractFindsByAttribute)attribute;

                var findsByAttribute = attribute as FindsByAttribute;
                if (findsByAttribute != null && findsByAttribute.Using == null)
                {
                    findsByAttribute.Using = member.Name;
                }

                bys.Add(castedAttribute.Finder);
            }

            if (useSequence)
            {
                ByChained chained = new ByChained(bys.ToArray());
                bys.Clear();
                bys.Add(chained);
            }

            if (useAll)
            {
                ByAll all = new ByAll(bys.ToArray());
                bys.Clear();
                bys.Add(all);
            }
        }

        return bys.AsReadOnly();
    }
}
