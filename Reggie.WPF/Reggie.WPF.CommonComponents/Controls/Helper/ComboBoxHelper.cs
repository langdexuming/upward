/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/7/18 19:32:09
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Reggie.WPF.CommonComponents.Controls
{
    /// <summary>
    /// A helper class that provides various attached properties for the <see cref="ComboBox"/> control.
    /// </summary>
    public class ComboBoxHelper
    {
        public static readonly DependencyProperty EnableVirtualizationWithGroupingProperty
            = DependencyProperty.RegisterAttached("EnableVirtualizationWithGrouping",
                                                  typeof(bool),
                                                  typeof(ComboBoxHelper),
                                                  new FrameworkPropertyMetadata(false, OnEnableVirtualizationWithGroupingChanged));

        private static void OnEnableVirtualizationWithGroupingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = dependencyObject as ComboBox;
            if (comboBox != null && e.NewValue != e.OldValue)
            {
#if NET4_5
                comboBox.SetCurrentValue(VirtualizingStackPanel.IsVirtualizingProperty, e.NewValue);
                comboBox.SetCurrentValue(VirtualizingPanel.IsVirtualizingWhenGroupingProperty, e.NewValue);
                comboBox.SetCurrentValue(ScrollViewer.CanContentScrollProperty, e.NewValue);
#endif
            }
        }

        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static void SetEnableVirtualizationWithGrouping(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableVirtualizationWithGroupingProperty, value);
        }

        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static bool GetEnableVirtualizationWithGrouping(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableVirtualizationWithGroupingProperty);
        }

        public static readonly DependencyProperty MaxLengthProperty
            = DependencyProperty.RegisterAttached("MaxLength",
                                                  typeof(int),
                                                  typeof(ComboBoxHelper),
                                                  new FrameworkPropertyMetadata(0),
                                                  ValidateMaxLength);

        private static bool ValidateMaxLength(object value)
        {
            return ((int)value) >= 0;
        }

        /// <summary>
        /// Gets the Maximum number of characters the TextBox can accept.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static int GetMaxLength(UIElement obj)
        {
            return (int)obj.GetValue(MaxLengthProperty);
        }

        /// <summary>
        /// Sets the Maximum number of characters the TextBox can accept.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static void SetMaxLength(UIElement obj, int value)
        {
            obj.SetValue(MaxLengthProperty, value);
        }

        public static readonly DependencyProperty CharacterCasingProperty
            = DependencyProperty.RegisterAttached("CharacterCasing",
                                                  typeof(CharacterCasing),
                                                  typeof(ComboBoxHelper),
                                                  new FrameworkPropertyMetadata(CharacterCasing.Normal),
                                                  ValidateCharacterCasing);

        private static bool ValidateCharacterCasing(object value)
        {
            return (CharacterCasing.Normal <= (CharacterCasing)value && (CharacterCasing)value <= CharacterCasing.Upper);
        }

        /// <summary>
        /// Gets the Character casing of the TextBox.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static CharacterCasing GetCharacterCasing(UIElement obj)
        {
            return (CharacterCasing)obj.GetValue(CharacterCasingProperty);
        }

        /// <summary>
        /// Sets the Character casing of the TextBox.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static void SetCharacterCasing(UIElement obj, CharacterCasing value)
        {
            obj.SetValue(CharacterCasingProperty, value);
        }


        /// <summary>
        /// Sets the Character casing of the TextBox.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static DataTemplate GetSelectionBoxItemTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(SelectionBoxItemTemplateProperty);
        }

        /// <summary>
        /// Sets the Character casing of the TextBox.
        /// </summary>
        [Category(AppName.CommonComponents)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static void SetSelectionBoxItemTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(SelectionBoxItemTemplateProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBoxItemTemplateProperty =
            DependencyProperty.RegisterAttached("SelectionBoxItemTemplate", typeof(DataTemplate), typeof(ComboBoxHelper), new PropertyMetadata(default(DataTemplate)));

    }
}
