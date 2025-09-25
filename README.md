# 見積登録システム

## 概要
.NET 9、WPF、Infragistics Professional、CommunityToolkit.Mvvmを使用した見積登録画面です。
1レコードを複数行として扱い、行選択、コピー・貼り付け（ディープコピー）機能を提供します。

## 主な機能

### 基本機能
- **見積基本情報管理**: 見積番号、見積日、顧客名、有効期限
- **項目管理**: 項目コード、項目名、説明、単価、数量、割引、納期、備考
- **自動計算**: 小計、合計金額の自動計算
- **バリデーション**: 入力内容の検証機能

### 行操作機能
- **行選択**: チェックボックスまたは行クリックで選択
- **複数行選択**: 複数項目の一括操作に対応
- **行移動**: 選択項目の上下移動
- **コピー・貼り付け**: ディープコピーによる項目複製
- **項目削除**: 選択項目の一括削除
- **項目複製**: 選択項目の複製

### UI機能
- **メニューバー**: ファイル、編集、ツールメニュー
- **ツールバー**: よく使う機能へのクイックアクセス
- **キーボードショートカット**: 効率的な操作
- **統計表示**: 項目数、選択数、合計金額の表示

## キーボードショートカット

| ショートカット | 機能 |
|---------------|------|
| Ctrl+N | 新規作成 |
| Ctrl+S | 保存 |
| Ctrl+C | コピー |
| Ctrl+V | 貼り付け |
| Ctrl+A | 全選択/解除 |
| Ctrl+D | 複製 |
| Delete | 削除 |
| Ctrl+P | 印刷プレビュー |
| F5 | バリデーション |
| Ctrl+↑ | 項目を上に移動 |
| Ctrl+↓ | 項目を下に移動 |

## プロジェクト構成

```
EstimateApp/
├── Models/
│   └── EstimateItem.cs          # 見積項目のモデル
├── ViewModels/
│   └── EstimateRegistrationViewModel.cs # メインViewModel
├── Converters/
│   └── ValueConverters.cs       # UI用のValueConverter
├── MainWindow.xaml              # メインUI
├── MainWindow.xaml.cs           # CodeBehind
├── App.xaml                     # アプリケーション定義
├── App.xaml.cs                  # アプリケーションCodeBehind
└── EstimateApp.csproj          # プロジェクトファイル
```

## 必要なパッケージ

```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
<PackageReference Include="Infragistics.WPF" Version="24.1.20241.20" />
```

## 使用方法

### 基本操作
1. **新規項目追加**: 「項目追加」ボタンまたはCtrl+Nで新しい項目を追加
2. **項目編集**: 各フィールドに直接入力して編集
3. **項目選択**: チェックボックスまたは行をクリックして選択
4. **項目削除**: 項目を選択してDeleteキーまたは「削除」ボタン

### コピー・貼り付け操作
1. **コピー**: 項目を選択してCtrl+Cまたは「コピー」ボタン
2. **貼り付け**: 
   - 項目を選択している場合: 選択項目にデータを上書き
   - 未選択の場合: 新しい項目として追加

### 項目移動
1. 移動したい項目を1つ選択
2. Ctrl+↑で上に移動、Ctrl+↓で下に移動

## MVVMパターンの実装

### Model (EstimateItem.cs)
- `ObservableObject`を継承したデータモデル
- `ICloneable`インターフェイスによるディープコピー対応
- 計算プロパティによる小計の自動更新

### ViewModel (EstimateRegistrationViewModel.cs)
- `ObservableObject`を継承したViewModelクラス
- `RelayCommand`による操作コマンドの実装
- `ObservableCollection`によるデータバインディング

### View (MainWindow.xaml)
- Infragisticsコントロールを使用したUI
- MVVMパターンに基づくデータバインディング
- 行選択状態に応じたスタイリング

## カスタマイズ

### 新しいフィールドの追加
1. `EstimateItem.cs`にプロパティを追加
2. `Clone()`メソッドと`CopyFrom()`メソッドを更新
3. UI（MainWindow.xaml）にコントロールを追加

### バリデーションルールの追加
1. `ValidateEstimate()`メソッドに検証ロジックを追加
2. 必要に応じてValueConverterを実装

### 保存・読み込み機能の実装
1. `SaveEstimate()`、`ImportEstimate()`、`ExportEstimate()`メソッドに実装
2. JSON、XML、データベースなど任意の形式に対応可能

## 注意事項

- Infragistics Professionalライセンスが必要です
- .NET 9環境での動作を前提としています
- 実際の保存・読み込み機能は実装例のため、実際の用途に応じて実装してください

## ライセンス

このコードは教育・学習目的で提供されています。
実際のプロジェクトで使用する場合は、適切なライセンス条項を追加してください。