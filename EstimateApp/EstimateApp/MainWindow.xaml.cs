using System.Windows;
using System.Windows.Input;
using EstimateApp.ViewModels;
using EstimateApp.Models;

namespace EstimateApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public EstimateRegistrationViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new EstimateRegistrationViewModel();
            DataContext = ViewModel;

            // キーボードショートカットを設定
            SetupKeyboardShortcuts();
        }

        /// <summary>
        /// キーボードショートカットを設定
        /// </summary>
        private void SetupKeyboardShortcuts()
        {
            // Ctrl+C でコピー
            var copyGesture = new KeyGesture(Key.C, ModifierKeys.Control);
            var copyBinding = new KeyBinding(ViewModel.CopySelectedItemCommand, copyGesture);
            this.InputBindings.Add(copyBinding);

            // Ctrl+V で貼り付け
            var pasteGesture = new KeyGesture(Key.V, ModifierKeys.Control);
            var pasteBinding = new KeyBinding(ViewModel.PasteItemCommand, pasteGesture);
            this.InputBindings.Add(pasteBinding);

            // Delete で削除
            var deleteGesture = new KeyGesture(Key.Delete);
            var deleteBinding = new KeyBinding(ViewModel.DeleteSelectedItemsCommand, deleteGesture);
            this.InputBindings.Add(deleteBinding);

            // Ctrl+A で全選択
            var selectAllGesture = new KeyGesture(Key.A, ModifierKeys.Control);
            var selectAllBinding = new KeyBinding(ViewModel.ToggleAllSelectionCommand, selectAllGesture);
            this.InputBindings.Add(selectAllBinding);

            // Ctrl+N で新規追加
            var addGesture = new KeyGesture(Key.N, ModifierKeys.Control);
            var addBinding = new KeyBinding(ViewModel.AddItemCommand, addGesture);
            this.InputBindings.Add(addBinding);

            // Ctrl+S で保存
            var saveGesture = new KeyGesture(Key.S, ModifierKeys.Control);
            var saveBinding = new KeyBinding(ViewModel.SaveEstimateCommand, saveGesture);
            this.InputBindings.Add(saveBinding);

            // Ctrl+D で複製
            var duplicateGesture = new KeyGesture(Key.D, ModifierKeys.Control);
            var duplicateBinding = new KeyBinding(ViewModel.DuplicateSelectedItemsCommand, duplicateGesture);
            this.InputBindings.Add(duplicateBinding);

            // Ctrl+P で印刷プレビュー
            var printGesture = new KeyGesture(Key.P, ModifierKeys.Control);
            var printBinding = new KeyBinding(ViewModel.PrintPreviewCommand, printGesture);
            this.InputBindings.Add(printBinding);

            // F5 でバリデーション
            var validateGesture = new KeyGesture(Key.F5);
            var validateBinding = new KeyBinding(ViewModel.ValidateEstimateCommand, validateGesture);
            this.InputBindings.Add(validateBinding);

            // Ctrl+↑ で項目を上に移動
            var moveUpGesture = new KeyGesture(Key.Up, ModifierKeys.Control);
            var moveUpBinding = new KeyBinding(ViewModel.MoveSelectedItemUpCommand, moveUpGesture);
            this.InputBindings.Add(moveUpBinding);

            // Ctrl+↓ で項目を下に移動
            var moveDownGesture = new KeyGesture(Key.Down, ModifierKeys.Control);
            var moveDownBinding = new KeyBinding(ViewModel.MoveSelectedItemDownCommand, moveDownGesture);
            this.InputBindings.Add(moveDownBinding);
        }

        /// <summary>
        /// 入力コントロールにフォーカスが当たったときに行選択を解除
        /// </summary>
        private void InputControl_GotFocus(object sender, RoutedEventArgs e)
        {
            // フォーカスされたコントロールの親EstimateItem以外の選択を解除
            if (sender is FrameworkElement element && element.DataContext is EstimateItem currentItem)
            {
                foreach (var item in ViewModel.EstimateItems)
                {
                    if (item.IsSelected)
                    {
                        item.IsSelected = false;
                    }
                }
            }
        }

        /// <summary>
        /// メニュー - 終了
        /// </summary>
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("アプリケーションを終了しますか？\n保存されていないデータは失われます。",
                "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// ウィンドウを閉じる際の確認
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("アプリケーションを終了しますか？\n保存されていないデータは失われます。",
                "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

            base.OnClosing(e);
        }
    }
}