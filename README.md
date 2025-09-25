# ���ϓo�^�V�X�e��

## �T�v
.NET 9�AWPF�AInfragistics Professional�ACommunityToolkit.Mvvm���g�p�������ϓo�^��ʂł��B
1���R�[�h�𕡐��s�Ƃ��Ĉ����A�s�I���A�R�s�[�E�\��t���i�f�B�[�v�R�s�[�j�@�\��񋟂��܂��B

## ��ȋ@�\

### ��{�@�\
- **���ϊ�{���Ǘ�**: ���ϔԍ��A���ϓ��A�ڋq���A�L������
- **���ڊǗ�**: ���ڃR�[�h�A���ږ��A�����A�P���A���ʁA�����A�[���A���l
- **�����v�Z**: ���v�A���v���z�̎����v�Z
- **�o���f�[�V����**: ���͓��e�̌��؋@�\

### �s����@�\
- **�s�I��**: �`�F�b�N�{�b�N�X�܂��͍s�N���b�N�őI��
- **�����s�I��**: �������ڂ̈ꊇ����ɑΉ�
- **�s�ړ�**: �I�����ڂ̏㉺�ړ�
- **�R�s�[�E�\��t��**: �f�B�[�v�R�s�[�ɂ�鍀�ڕ���
- **���ڍ폜**: �I�����ڂ̈ꊇ�폜
- **���ڕ���**: �I�����ڂ̕���

### UI�@�\
- **���j���[�o�[**: �t�@�C���A�ҏW�A�c�[�����j���[
- **�c�[���o�[**: �悭�g���@�\�ւ̃N�C�b�N�A�N�Z�X
- **�L�[�{�[�h�V���[�g�J�b�g**: �����I�ȑ���
- **���v�\��**: ���ڐ��A�I�𐔁A���v���z�̕\��

## �L�[�{�[�h�V���[�g�J�b�g

| �V���[�g�J�b�g | �@�\ |
|---------------|------|
| Ctrl+N | �V�K�쐬 |
| Ctrl+S | �ۑ� |
| Ctrl+C | �R�s�[ |
| Ctrl+V | �\��t�� |
| Ctrl+A | �S�I��/���� |
| Ctrl+D | ���� |
| Delete | �폜 |
| Ctrl+P | ����v���r���[ |
| F5 | �o���f�[�V���� |
| Ctrl+�� | ���ڂ���Ɉړ� |
| Ctrl+�� | ���ڂ����Ɉړ� |

## �v���W�F�N�g�\��

```
EstimateApp/
������ Models/
��   ������ EstimateItem.cs          # ���ύ��ڂ̃��f��
������ ViewModels/
��   ������ EstimateRegistrationViewModel.cs # ���C��ViewModel
������ Converters/
��   ������ ValueConverters.cs       # UI�p��ValueConverter
������ MainWindow.xaml              # ���C��UI
������ MainWindow.xaml.cs           # CodeBehind
������ App.xaml                     # �A�v���P�[�V������`
������ App.xaml.cs                  # �A�v���P�[�V����CodeBehind
������ EstimateApp.csproj          # �v���W�F�N�g�t�@�C��
```

## �K�v�ȃp�b�P�[�W

```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
<PackageReference Include="Infragistics.WPF" Version="24.1.20241.20" />
```

## �g�p���@

### ��{����
1. **�V�K���ڒǉ�**: �u���ڒǉ��v�{�^���܂���Ctrl+N�ŐV�������ڂ�ǉ�
2. **���ڕҏW**: �e�t�B�[���h�ɒ��ړ��͂��ĕҏW
3. **���ڑI��**: �`�F�b�N�{�b�N�X�܂��͍s���N���b�N���đI��
4. **���ڍ폜**: ���ڂ�I������Delete�L�[�܂��́u�폜�v�{�^��

### �R�s�[�E�\��t������
1. **�R�s�[**: ���ڂ�I������Ctrl+C�܂��́u�R�s�[�v�{�^��
2. **�\��t��**: 
   - ���ڂ�I�����Ă���ꍇ: �I�����ڂɃf�[�^���㏑��
   - ���I���̏ꍇ: �V�������ڂƂ��Ēǉ�

### ���ڈړ�
1. �ړ����������ڂ�1�I��
2. Ctrl+���ŏ�Ɉړ��ACtrl+���ŉ��Ɉړ�

## MVVM�p�^�[���̎���

### Model (EstimateItem.cs)
- `ObservableObject`���p�������f�[�^���f��
- `ICloneable`�C���^�[�t�F�C�X�ɂ��f�B�[�v�R�s�[�Ή�
- �v�Z�v���p�e�B�ɂ�鏬�v�̎����X�V

### ViewModel (EstimateRegistrationViewModel.cs)
- `ObservableObject`���p������ViewModel�N���X
- `RelayCommand`�ɂ�鑀��R�}���h�̎���
- `ObservableCollection`�ɂ��f�[�^�o�C���f�B���O

### View (MainWindow.xaml)
- Infragistics�R���g���[�����g�p����UI
- MVVM�p�^�[���Ɋ�Â��f�[�^�o�C���f�B���O
- �s�I����Ԃɉ������X�^�C�����O

## �J�X�^�}�C�Y

### �V�����t�B�[���h�̒ǉ�
1. `EstimateItem.cs`�Ƀv���p�e�B��ǉ�
2. `Clone()`���\�b�h��`CopyFrom()`���\�b�h���X�V
3. UI�iMainWindow.xaml�j�ɃR���g���[����ǉ�

### �o���f�[�V�������[���̒ǉ�
1. `ValidateEstimate()`���\�b�h�Ɍ��؃��W�b�N��ǉ�
2. �K�v�ɉ�����ValueConverter������

### �ۑ��E�ǂݍ��݋@�\�̎���
1. `SaveEstimate()`�A`ImportEstimate()`�A`ExportEstimate()`���\�b�h�Ɏ���
2. JSON�AXML�A�f�[�^�x�[�X�ȂǔC�ӂ̌`���ɑΉ��\

## ���ӎ���

- Infragistics Professional���C�Z���X���K�v�ł�
- .NET 9���ł̓����O��Ƃ��Ă��܂�
- ���ۂ̕ۑ��E�ǂݍ��݋@�\�͎�����̂��߁A���ۂ̗p�r�ɉ����Ď������Ă�������

## ���C�Z���X

���̃R�[�h�͋���E�w�K�ړI�Œ񋟂���Ă��܂��B
���ۂ̃v���W�F�N�g�Ŏg�p����ꍇ�́A�K�؂ȃ��C�Z���X������ǉ����Ă��������B