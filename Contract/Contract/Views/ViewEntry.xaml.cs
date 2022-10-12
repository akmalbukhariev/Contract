using Contract.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEntry : ContentView
    {
        #region PlaceHoldingText
        public static readonly BindableProperty PlaceHoldingTextProperty =
            BindableProperty.Create(nameof(PlaceHoldingText),
                                    typeof(string),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: PlaceHoldingTextPropertyChanged);

        public string PlaceHoldingText
        {
            get { return (string)GetValue(PlaceHoldingTextProperty); }
            set { SetValue(PlaceHoldingTextProperty, value); }
        }

        private static void PlaceHoldingTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (control != null)
                control.entry.Placeholder = newValue.ToString();
        }
        #endregion
         
        #region FontSize
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),
                                    typeof(double),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: FontSizePropertyChanged);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        private static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (control != null)
                control.entry.FontSize = (double)newValue;
        }
        #endregion

        #region FontFamily
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily),
                                    typeof(string),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: FontFamilyPropertyChanged);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        private static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (control != null)
                control.entry.FontFamily = (string)newValue;
        }
        #endregion

        #region Text
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(ViewEntry),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region HeightEntryView
        public static readonly BindableProperty HeightEntryViewProperty =
           BindableProperty.Create(nameof(HeightEntryView),
                                   typeof(string),
                                   typeof(ViewEntry),
                                   null,
                                   propertyChanged: HeightEntryViewPropertyChanged);
         
        public int HeightEntryView
        {
            get { return (int)GetValue(HeightEntryViewProperty); }
            set { SetValue(HeightEntryViewProperty, value); }
        }

        private static void HeightEntryViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (control != null)
                control.grMain.HeightRequest = double.Parse((string)newValue);
        }
        #endregion

        #region CornerRadius
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius),
                                    typeof(double),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: CornerRadiusPropertyChanged);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        private static void CornerRadiusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.backBoxView.CornerRadius = new CornerRadius((double)newValue);
                control.frontBoxView.CornerRadius = new CornerRadius((double)newValue);
            }
        }
        #endregion

        #region Border width
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth),
                                    typeof(double),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: BorderWidthPropertyChanged);

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        private static void BorderWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                double val = (double)newValue;
                control.frontBoxView.Margin = new Thickness(val);
            }
        }
        #endregion

        #region Background color
        public static readonly BindableProperty BackgroundColorOfEntryProperty =
            BindableProperty.Create(nameof(BackgroundColorOfEntry),
                                    typeof(Color),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: BackgroundColorOfEntryPropertyChanged);

        public Color BackgroundColorOfEntry
        {
            get { return (Color)GetValue(BackgroundColorOfEntryProperty); }
            set { SetValue(BackgroundColorOfEntryProperty, value); }
        }

        private static void BackgroundColorOfEntryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.frontBoxView.BackgroundColor = (Color)newValue;
            }
        }
        #endregion

        #region Border color
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor),
                                    typeof(Color),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: BorderColorPropertyChanged);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        private static void BorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.backBoxView.BackgroundColor = (Color)newValue;
            }
        }
        #endregion

        #region Place holder text color
        public static readonly BindableProperty PlaceHolderTextColorProperty =
            BindableProperty.Create(nameof(PlaceHolderTextColor),
                                    typeof(Color),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: PlaceHolderTextColorPropertyChanged);

        public Color PlaceHolderTextColor
        {
            get { return (Color)GetValue(PlaceHolderTextColorProperty); }
            set { SetValue(PlaceHolderTextColorProperty, value); }
        }

        private static void PlaceHolderTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.entry.PlaceholderColor = (Color)newValue;
            }
        }
        #endregion

        #region Text color
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),
                                    typeof(Color),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: TextColorPropertyChanged);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.entry.TextColor = (Color)newValue; 
            }
        }
        #endregion
          
        #region MaxLength
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength),
                                    typeof(int),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: MaxLengthPropertyChanged);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        private static void MaxLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.entry.MaxLength = (int)newValue;
            }
        }
        #endregion

        #region Vertical TextAlignment
        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment),
                                    typeof(TextAlignment),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: VerticalTextAlignmentPropertyChanged);

        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(VerticalTextAlignmentProperty, value); }
        }

        private static void VerticalTextAlignmentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.entry.VerticalTextAlignment = (TextAlignment)newValue;
            }
        }
        #endregion

        #region Horizontal TextAlignment
        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment),
                                    typeof(TextAlignment),
                                    typeof(ViewEntry),
                                    null,
                                    propertyChanged: HorizontalTextAlignmentPropertyChanged);

        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        private static void HorizontalTextAlignmentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewEntry)bindable;
            if (newValue != null)
            {
                control.entry.HorizontalTextAlignment = (TextAlignment)newValue;
            }
        }
        #endregion

        #region IsPassword
        public static readonly BindableProperty IsPasswordProperty =
           BindableProperty.Create(nameof(IsPassword),
                                   typeof(bool),
                                   typeof(ViewEntry),
                                   defaultValue: false,
                                   defaultBindingMode: BindingMode.TwoWay);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        #endregion

        #region Keyboard
        public static readonly BindableProperty KeyboardSettingProperty =
           BindableProperty.Create(nameof(KeyboardSetting),
                                   typeof(Keyboard),
                                   typeof(ViewEntry),
                                   defaultValue: Keyboard.Default,
                                   defaultBindingMode: BindingMode.TwoWay);

        public Keyboard KeyboardSetting
        {
            get { return (Keyboard)GetValue(KeyboardSettingProperty); }
            set { SetValue(KeyboardSettingProperty, value); }
        }
        #endregion

        #region TextChanged
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(ViewEntry), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(ViewEntry), null);

        public event EventHandler<TextChangedEventArgs> TextChanged;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        public Entry Entry => entry;
          
        public ViewEntry()
        {
            InitializeComponent();

            this.entry.TextChanged += Entry_TextChanged;

            this.entry.TextColorForIOS = Color.White;
            this.entry.SetBinding(Entry.KeyboardProperty, new Binding(nameof(KeyboardSetting), source: this));
            this.entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this));
            this.entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ControlApp.Instance.ChangedCurrentPrice();
            #region 
            //object resolvedParameter;

            //if (CommandParameter != null)
            //{
            //    resolvedParameter = CommandParameter;
            //}
            //else
            //{
            //    resolvedParameter = e;
            //}

            //if (Command?.CanExecute(resolvedParameter) ?? true)
            //{
            //    TextChanged?.Invoke(this, e);
            //    Command?.Execute(resolvedParameter);
            //}
            #endregion
        }
    }
}