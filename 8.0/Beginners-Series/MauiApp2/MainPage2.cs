using Microsoft.Maui.Controls;
using MauiApp2.ViewModel;

namespace MauiApp2;

public class MainPage2 : ContentPage
{
    public MainPage2(MainViewModel vm)
    {
        BindingContext = vm;

        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = 100 },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(0.75, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(0.25, GridUnitType.Star) }
            },
            Padding = 10,
            RowSpacing = 10,
            ColumnSpacing = 10
        };

        var image = new Image { Source = "logo.png", BackgroundColor = Colors.Transparent };
        grid.Add(image, 0, 0);
        Grid.SetColumnSpan(image, 2);

        var entry = new Entry { Placeholder = "Enter task" };
        entry.SetBinding(Entry.TextProperty, "Text");
        grid.Add(entry, 0, 1);

        var addButton = new Button { Text = "Add" };
        addButton.SetBinding(Button.CommandProperty, "AddCommand");
        grid.Add(addButton, 1, 1);

        var collectionView = new CollectionView { SelectionMode = SelectionMode.None };
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, "Items");
        // ItemTemplate and SwipeView setup omitted for brevity

        collectionView.ItemTemplate = new DataTemplate(() =>
        {
            var swipeItem = new SwipeItem
            {
                Text = "Delete",
                BackgroundColor = Colors.Red,
                // Command binding should be set here
            };
            swipeItem.SetBinding(SwipeItem.CommandProperty, new Binding("DeleteCommand", source: BindingContext));
            swipeItem.SetBinding(SwipeItem.CommandParameterProperty, new Binding("."));

            var swipeItems = new SwipeItems { swipeItem };
            swipeItems.Mode = SwipeMode.Execute;

            var swipeView = new SwipeView
            {
                RightItems = swipeItems
            };

            var CustomLabelStyle = MauiControlUtils.GetStyleFromMergedDictionaries("CustomLabelStyle");

            var label = new Label { FontSize = 24, Style = CustomLabelStyle };
            label.SetBinding(Label.TextProperty, new Binding("."));

            var frame = new Frame { Content = label };

            var tapGestureRecognizer = new TapGestureRecognizer();
            // MainViewModelのTapCommandにバインド
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty,
                new Binding("TapCommand", source: BindingContext));
            // コマンドパラメータに現在のデータコンテキスト（.）をバインド
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty,
                new Binding("."));

            frame.GestureRecognizers.Add(tapGestureRecognizer);

            swipeView.Content = frame;

            return swipeView;
        });


        grid.Add(collectionView, 0, 2);
        Grid.SetColumnSpan(collectionView, 2);

        Content = grid;
    }
}
