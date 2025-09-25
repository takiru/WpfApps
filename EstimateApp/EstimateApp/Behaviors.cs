using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using EstimateApp.Models;
using EstimateApp.ViewModels;

namespace EstimateApp.Behaviors
{
    /// <summary>
    /// 行選択のためのビヘイビア
    /// </summary>
    public class SelectionBehavior : Behavior<ItemsControl>
    {
        private bool _isDragging = false;
        private EstimateItem? _dragStartItem = null;
        private EstimateItem? _lastSelectedItem = null;

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(EstimateRegistrationViewModel),
                typeof(SelectionBehavior));

        public EstimateRegistrationViewModel ViewModel
        {
            get => (EstimateRegistrationViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += OnPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove -= OnPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
            AssociatedObject.MouseLeave -= OnMouseLeave;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = GetEstimateItemFromPoint(e.GetPosition(AssociatedObject));
            if (item == null) return;

            var indicator = GetIndicatorFromPoint(e.GetPosition(AssociatedObject));
            if (indicator == null) return; // インジケーター領域外ならスルー

            // インジケーターをクリックしたときはItemsControlにフォーカスを移す
            AssociatedObject.Focus();

            _dragStartItem = item;
            _isDragging = false;

            var isCtrlPressed = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
            var isShiftPressed = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);

            if (isCtrlPressed)
            {
                // Ctrl+クリック: 個別選択/解除
                item.IsSelected = !item.IsSelected;
                if (item.IsSelected)
                {
                    _lastSelectedItem = item;
                }
            }
            else if (isShiftPressed && _lastSelectedItem != null)
            {
                // Shift+クリック: 範囲選択
                SelectRange(_lastSelectedItem, item);
            }
            else
            {
                // 通常クリック: 単一選択
                ClearAllSelections();
                item.IsSelected = true;
                _lastSelectedItem = item;

                // ドラッグ開始の準備
                AssociatedObject.CaptureMouse();
            }

            e.Handled = true;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragStartItem == null || !AssociatedObject.IsMouseCaptured) return;

            var currentItem = GetEstimateItemFromPoint(e.GetPosition(AssociatedObject));
            if (currentItem == null) return;

            if (!_isDragging)
            {
                _isDragging = true;
                ClearAllSelections();
            }

            // ドラッグ範囲選択
            SelectRange(_dragStartItem, currentItem);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured)
            {
                AssociatedObject.ReleaseMouseCapture();
            }

            _isDragging = false;
            _dragStartItem = null;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured)
            {
                AssociatedObject.ReleaseMouseCapture();
            }

            _isDragging = false;
            _dragStartItem = null;
        }

        private EstimateItem? GetEstimateItemFromPoint(Point point)
        {
            var hitTest = VisualTreeHelper.HitTest(AssociatedObject, point);
            if (hitTest?.VisualHit == null) return null;

            var element = hitTest.VisualHit as FrameworkElement;
            while (element != null)
            {
                if (element.DataContext is EstimateItem item)
                {
                    return item;
                }
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }

            return null;
        }

        private FrameworkElement? GetIndicatorFromPoint(Point point)
        {
            var hitTest = VisualTreeHelper.HitTest(AssociatedObject, point);
            if (hitTest?.VisualHit == null) return null;

            var element = hitTest.VisualHit as FrameworkElement;
            while (element != null)
            {
                if (element.Name == "SelectionIndicator")
                {
                    return element;
                }
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }

            return null;
        }

        private void SelectRange(EstimateItem startItem, EstimateItem endItem)
        {
            if (ViewModel?.EstimateItems == null) return;

            var startIndex = ViewModel.EstimateItems.IndexOf(startItem);
            var endIndex = ViewModel.EstimateItems.IndexOf(endItem);

            if (startIndex == -1 || endIndex == -1) return;

            var minIndex = Math.Min(startIndex, endIndex);
            var maxIndex = Math.Max(startIndex, endIndex);

            for (int i = minIndex; i <= maxIndex; i++)
            {
                ViewModel.EstimateItems[i].IsSelected = true;
            }
        }

        private void ClearAllSelections()
        {
            if (ViewModel?.EstimateItems == null) return;

            foreach (var item in ViewModel.EstimateItems)
            {
                item.IsSelected = false;
            }
        }
    }
}