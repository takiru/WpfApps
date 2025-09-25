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
    /// 見積登録画面のViewModel
    /// </summary>
    public partial class EstimateRegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EstimateItem> estimateItems = new();

        [ObservableProperty]
        private string estimateNumber = string.Empty;

        [ObservableProperty]
        private DateTime estimateDate = DateTime.Today;

        [ObservableProperty]
        private string customerName = string.Empty;

        [ObservableProperty]
        private DateTime validUntil = DateTime.Today.AddDays(30);

        private List<EstimateItem>? _clipboardItems;

        /// <summary>
        /// 合計金額（計算プロパティ）
        /// </summary>
        public decimal TotalAmount => EstimateItems.Sum(item => item.Subtotal);

        public EstimateRegistrationViewModel()
        {
            InitializeData();

            // コレクションの変更を監視して合計金額の更新通知を発行
            EstimateItems.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (EstimateItem item in e.OldItems)
                    {
                        item.PropertyChanged -= Item_PropertyChanged;
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (EstimateItem item in e.NewItems)
                    {
                        item.PropertyChanged += Item_PropertyChanged;
                    }
                }

                OnPropertyChanged(nameof(TotalAmount));
            };
        }

        private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EstimateItem.Subtotal))
            {
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        /// <summary>
        /// 初期データを設定
        /// </summary>
        private void InitializeData()
        {
            EstimateNumber = $"EST-{DateTime.Now:yyyyMMdd}-001";
            CustomerName = "サンプル顧客";

            // サンプルデータを追加
            EstimateItems.Add(new EstimateItem
            {
                ItemCode = "ITM001",
                ItemName = "システム開発",
                Description = "業務システムの設計・開発",
                UnitPrice = 500000,
                Quantity = 1,
                Discount = 0,
                DeliveryDate = DateTime.Today.AddDays(60),
                Remarks = "要件定義から実装まで"
            });

            EstimateItems.Add(new EstimateItem
            {
                ItemCode = "ITM002",
                ItemName = "システム保守",
                Description = "月次保守サポート",
                UnitPrice = 50000,
                Quantity = 12,
                Discount = 50000,
                DeliveryDate = DateTime.Today.AddDays(30),
                Remarks = "1年間のサポート契約"
            });
        }

        /// <summary>
        /// 新しい項目を追加
        /// </summary>
        [RelayCommand]
        private void AddItem()
        {
            var newItem = new EstimateItem
            {
                ItemCode = $"ITM{EstimateItems.Count + 1:D3}",
                DeliveryDate = DateTime.Today.AddDays(30)
            };

            EstimateItems.Add(newItem);
        }

        /// <summary>
        /// 選択された項目を削除
        /// </summary>
        [RelayCommand]
        private void DeleteSelectedItems()
        {
            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("削除する項目を選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"{selectedItems.Count}件の項目を削除しますか？", "削除確認",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in selectedItems)
                {
                    EstimateItems.Remove(item);
                }
            }
        }

        /// <summary>
        /// 選択された項目をコピー（複数項目対応）
        /// </summary>
        [RelayCommand]
        private void CopySelectedItem()
        {
            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("コピーする項目を選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _clipboardItems = selectedItems.Select(item => item.Clone() as EstimateItem)
                                          .Where(item => item != null)
                                          .Cast<EstimateItem>()
                                          .ToList();

            MessageBox.Show($"{selectedItems.Count}件の項目をコピーしました。", "コピー完了",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// クリップボードの項目を貼り付け
        /// </summary>
        [RelayCommand]
        private void PasteItem()
        {
            if (_clipboardItems == null || _clipboardItems.Count == 0)
            {
                MessageBox.Show("貼り付ける項目がありません。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selectedItems = EstimateItems.Where(item => item.IsSelected).ToList();

            if (selectedItems.Count > 0)
            {
                // 選択項目がある場合：最初の選択項目から順次貼り付け
                var startIndex = EstimateItems.IndexOf(selectedItems.First());
                PasteMultipleItems(startIndex);
            }
            else
            {
                // 選択項目がない場合：末尾に新しい項目として追加
                PasteMultipleItems(EstimateItems.Count);
            }
        }

        /// <summary>
        /// 複数項目を指定位置から貼り付け
        /// </summary>
        private void PasteMultipleItems(int startIndex)
        {
            if (_clipboardItems == null) return;

            int pasteCount = 0;

            for (int i = 0; i < _clipboardItems.Count; i++)
            {
                var targetIndex = startIndex + i;
                var sourceItem = _clipboardItems[i];

                if (targetIndex < EstimateItems.Count)
                {
                    // 既存項目に貼り付け
                    EstimateItems[targetIndex].CopyFrom(sourceItem);
                    pasteCount++;
                }
                else
                {
                    // 新しい項目として追加
                    var newItem = sourceItem.Clone() as EstimateItem;
                    if (newItem != null)
                    {
                        newItem.ItemCode = $"ITM{EstimateItems.Count + 1:D3}";
                        EstimateItems.Add(newItem);
                        pasteCount++;
                    }
                }
            }

            MessageBox.Show($"{pasteCount}件の項目を貼り付けました。", "貼り付け完了",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 全項目の選択状態を切り替え
        /// </summary>
        [RelayCommand]
        private void ToggleAllSelection()
        {
            bool hasSelected = EstimateItems.Any(item => item.IsSelected);

            foreach (var item in EstimateItems)
            {
                item.IsSelected = !hasSelected;
            }
        }

        /// <summary>
        /// 見積を保存
        /// </summary>
        [RelayCommand]
        private void SaveEstimate()
        {
            // 実際の保存処理をここに実装
            MessageBox.Show($"見積番号: {EstimateNumber}\n合計金額: {TotalAmount:C}\n\n保存処理は実装してください。",
                "保存", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}