using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EstimateApp.Models
{
    /// <summary>
    /// 見積項目のモデルクラス
    /// </summary>
    public partial class EstimateItem : ObservableObject, ICloneable
    {
        [ObservableProperty]
        private string itemCode = string.Empty;

        [ObservableProperty]
        private string itemName = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private decimal unitPrice = 0;

        [ObservableProperty]
        private int quantity = 1;

        [ObservableProperty]
        private decimal discount = 0;

        [ObservableProperty]
        private DateTime deliveryDate = DateTime.Today.AddDays(30);

        [ObservableProperty]
        private string remarks = string.Empty;

        [ObservableProperty]
        private bool isSelected = false;

        /// <summary>
        /// 小計（計算プロパティ）
        /// </summary>
        public decimal Subtotal => (UnitPrice * Quantity) - Discount;

        /// <summary>
        /// 単価または数量が変更された際に小計の変更通知を発行
        /// </summary>
        partial void OnUnitPriceChanged(decimal value)
        {
            OnPropertyChanged(nameof(Subtotal));
        }

        partial void OnQuantityChanged(int value)
        {
            OnPropertyChanged(nameof(Subtotal));
        }

        partial void OnDiscountChanged(decimal value)
        {
            OnPropertyChanged(nameof(Subtotal));
        }

        /// <summary>
        /// ディープコピーを行う
        /// </summary>
        /// <returns>コピーされたEstimateItemインスタンス</returns>
        public object Clone()
        {
            return new EstimateItem
            {
                ItemCode = this.ItemCode,
                ItemName = this.ItemName,
                Description = this.Description,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity,
                Discount = this.Discount,
                DeliveryDate = this.DeliveryDate,
                Remarks = this.Remarks,
                IsSelected = false // 選択状態はコピーしない
            };
        }

        /// <summary>
        /// 他のEstimateItemからデータをコピー
        /// </summary>
        /// <param name="source">コピー元のEstimateItem</param>
        public void CopyFrom(EstimateItem source)
        {
            if (source == null) return;

            ItemCode = source.ItemCode;
            ItemName = source.ItemName;
            Description = source.Description;
            UnitPrice = source.UnitPrice;
            Quantity = source.Quantity;
            Discount = source.Discount;
            DeliveryDate = source.DeliveryDate;
            Remarks = source.Remarks;
        }
    }
}