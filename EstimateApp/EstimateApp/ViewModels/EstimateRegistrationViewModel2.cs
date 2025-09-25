using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstimateApp.Models;

namespace EstimateApp.ViewModels
{
    /// <summary>
    /// EstimateRegistrationViewModelの追加コマンド部分
    /// </summary>
    public partial class EstimateRegistrationViewModel : ObservableObject
    {
        /// <summary>
        /// 項目の選択状態を切り替え（行クリック用）
        /// </summary>
        [RelayCommand]
        private void ToggleSelection(EstimateItem item)
        {
            if (item != null)
            {
                item.IsSelected = !item.IsSelected;
            }
        }

        /// <summary>
        /// 選択された項目を上に移動
        /// </summary>
        [RelayCommand]
        private void MoveSelectedItemUp()
        {
            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count != 1)
            {
                MessageBox.Show("移動する項目を1つだけ選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selectedItem = selectedItems.First();
            var currentIndex = EstimateItems.IndexOf(selectedItem);

            if (currentIndex > 0)
            {
                EstimateItems.Move(currentIndex, currentIndex - 1);
            }
        }

        /// <summary>
        /// 選択された項目を下に移動
        /// </summary>
        [RelayCommand]
        private void MoveSelectedItemDown()
        {
            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count != 1)
            {
                MessageBox.Show("移動する項目を1つだけ選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selectedItem = selectedItems.First();
            var currentIndex = EstimateItems.IndexOf(selectedItem);

            if (currentIndex < EstimateItems.Count - 1)
            {
                EstimateItems.Move(currentIndex, currentIndex + 1);
            }
        }

        /// <summary>
        /// 選択された項目を複製
        /// </summary>
        [RelayCommand]
        private void DuplicateSelectedItems()
        {
            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("複製する項目を選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var item in selectedItems)
            {
                var duplicatedItem = item.Clone() as EstimateItem;
                if (duplicatedItem != null)
                {
                    // 項目コードに連番を付与
                    duplicatedItem.ItemCode = $"{item.ItemCode}-COPY";
                    duplicatedItem.IsSelected = false;

                    // 元の項目の次に挿入
                    var originalIndex = EstimateItems.IndexOf(item);
                    EstimateItems.Insert(originalIndex + 1, duplicatedItem);
                }
            }

            MessageBox.Show($"{selectedItems.Count}件の項目を複製しました。", "複製完了",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 見積をクリア（新規作成）
        /// </summary>
        [RelayCommand]
        private void ClearEstimate()
        {
            var result = MessageBox.Show("現在の見積データをクリアして新規作成しますか？\n" +
                                       "保存されていないデータは失われます。", "新規作成確認",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                EstimateItems.Clear();
                EstimateNumber = $"EST-{DateTime.Now:yyyyMMdd}-001";
                EstimateDate = DateTime.Today;
                CustomerName = string.Empty;
                ValidUntil = DateTime.Today.AddDays(30);
                _clipboardItems = null;
            }
        }

        /// <summary>
        /// 見積をインポート（JSONファイルから読み込み）
        /// </summary>
        [RelayCommand]
        private void ImportEstimate()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                Title = "見積データファイルを選択してください"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // 実際の実装では、JSONファイルの読み込み処理を行う
                    MessageBox.Show($"インポート機能は実装してください。\nファイル: {openFileDialog.FileName}",
                        "インポート", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"インポートエラー: {ex.Message}", "エラー",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 見積をエクスポート（JSONファイルに保存）
        /// </summary>
        [RelayCommand]
        private void ExportEstimate()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                FileName = $"{EstimateNumber}.json",
                Title = "見積データの保存先を選択してください"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // 実際の実装では、JSONファイルの保存処理を行う
                    MessageBox.Show($"エクスポート機能は実装してください。\nファイル: {saveFileDialog.FileName}",
                        "エクスポート", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エクスポートエラー: {ex.Message}", "エラー",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 印刷プレビュー
        /// </summary>
        [RelayCommand]
        private void PrintPreview()
        {
            MessageBox.Show("印刷プレビュー機能は実装してください。", "印刷プレビュー",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        [RelayCommand]
        private void ValidateEstimate()
        {
            var errors = new System.Text.StringBuilder();

            // 基本情報のチェック
            if (string.IsNullOrWhiteSpace(EstimateNumber))
                errors.AppendLine("・見積番号が入力されていません。");

            if (string.IsNullOrWhiteSpace(CustomerName))
                errors.AppendLine("・顧客名が入力されていません。");

            if (EstimateDate > ValidUntil)
                errors.AppendLine("・有効期限が見積日より前に設定されています。");

            // 項目のチェック
            if (EstimateItems.Count == 0)
            {
                errors.AppendLine("・見積項目が登録されていません。");
            }
            else
            {
                for (int i = 0; i < EstimateItems.Count; i++)
                {
                    var item = EstimateItems[i];

                    if (string.IsNullOrWhiteSpace(item.ItemCode))
                        errors.AppendLine($"・項目{i + 1}: 項目コードが入力されていません。");

                    if (string.IsNullOrWhiteSpace(item.ItemName))
                        errors.AppendLine($"・項目{i + 1}: 項目名が入力されていません。");

                    if (item.UnitPrice < 0)
                        errors.AppendLine($"・項目{i + 1}: 単価が負の値になっています。");

                    if (item.Quantity <= 0)
                        errors.AppendLine($"・項目{i + 1}: 数量が0以下になっています。");

                    if (item.DeliveryDate < DateTime.Today)
                        errors.AppendLine($"・項目{i + 1}: 納期が過去の日付になっています。");
                }
            }

            if (errors.Length == 0)
            {
                MessageBox.Show("バリデーションチェックに問題はありませんでした。", "バリデーション結果",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"以下のエラーがあります:\n\n{errors}", "バリデーションエラー",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}