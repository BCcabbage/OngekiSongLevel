一个用于 ONGEKI 的 BepInEx 插件，可以局外显示歌曲定数等级。
该mod由AI生成,以下文案也基本由AI生成

## 📸 效果预览
还没拍

## 📦 安装方法

1. 确保你已经正确安装了BepInEx
2. 将 `SongLevel.dll` 放入 `SDDT/package/BepInEx/plugins/` 文件夹
3. enjoy


## ⚙️ 自定义配置
你可以在 `Plugin.cs` 中调整以下参数：

| 参数 | 位置 | 说明 |
|---|---|---|
| 字体大小 | `OnGUI()` → `style.fontSize` | 默认 20（1080p） |
| 文字颜色 | `OnGUI()` → `style.normal.textColor` | 默认白色 |
| 显示位置 | `OnGUI()` → `GUI.Label` 的 `Rect` | 默认屏幕中央偏下 |
| 显示内容 | `UpdateDisplay()` → `DisplayText = ...` | 自定义文字格式 |


### 引用依赖

编译时需要引用以下 DLL：
| DLL | 路径 |
|---|---|
| `BepInEx.dll` | `BepInEx/core/` |
| `0Harmony.dll` | `BepInEx/core/` |
| `Assembly-CSharp.dll` | `mu3_Data/Managed/` |
| `UnityEngine.dll` | `mu3_Data/Managed/` |



## 🐛 已知问题

- 部分魔改版本的游戏可能存在兼容性问题
- 4K 分辨率下位置偏移需手动调整（已提供等比缩放方案）


欢迎提交 Issue 和 Pull Request！

---

**如果这个 Mod 对你有帮助，请给一个 ⭐Star 支持一下！**
