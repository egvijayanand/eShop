using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

#nullable enable

namespace eShop.ClientApp.Extensions
{
    public static class BindableObjectExtensions
    {
        const string bindingContextPath = Binding.SelfPath;

        /// <summary>Binds to the default bindable property.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        public static TBindable Bindv2<TBindable, TBindingContext, TSource>(
            this TBindable bindable,
            Expression<Func<TBindingContext, TSource>> expression,
            BindingMode mode = BindingMode.Default,
            IValueConverter? converter = null,
            object? converterParameter = null,
            string? stringFormat = null,
            object? source = null,
            object? targetNullValue = null,
            object? fallbackValue = null)
            where TBindable : BindableObject
        {
            bindable.Bind(PropertyName(expression),
                          mode,
                          converter,
                          converterParameter,
                          stringFormat,
                          source,
                          targetNullValue,
                          fallbackValue);
            return bindable;
        }

        /// <summary>Binds to the specified bindable property.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        public static TBindable Bindv2<TBindable, TBindingContext, TSource>(
            this TBindable bindable,
            BindableProperty property,
            Expression<Func<TBindingContext, TSource>> expression,
            BindingMode mode = BindingMode.Default,
            IValueConverter? converter = null,
            object? converterParameter = null,
            string? stringFormat = null,
            object? source = null,
            object? targetNullValue = null,
            object? fallbackValue = null)
            where TBindable : BindableObject
        {
            bindable.Bind(property,
                          PropertyName(expression),
                          mode,
                          converter,
                          converterParameter,
                          stringFormat,
                          source,
                          targetNullValue,
                          fallbackValue);
            return bindable;
        }

        /// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        /// <param name="parameterPath">If null, no binding is created for the CommandParameter property.</param>
        public static TBindable BindCommandv2<TBindable, TBindingContext, TSource>(
            this TBindable bindable,
            Expression<Func<TBindingContext, TSource>> expression,
            object? source = null,
            string? parameterPath = bindingContextPath,
            object? parameterSource = null)
            where TBindable : BindableObject
            where TSource : ICommand
        {
            bindable.BindCommand(PropertyName(expression),
                                 source,
                                 parameterPath,
                                 parameterSource);
            return bindable;
        }
    }
}
