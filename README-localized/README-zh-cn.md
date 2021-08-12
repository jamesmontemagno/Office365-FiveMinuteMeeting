五分钟会议 - Office 365 和 Xamarin 示例
=========================

Office 365 联系人浏览器、编辑器和日历会议功能以及组织你的联系人。

若要了解详细信息，请查看 Ignite 2015 的 Office 365 会话：http://channel9.msdn.com/Events/Ignite/2015/BRK3342

## 警告
本示例使用的是 ADAL 的预览版本，并提到将来会有重大更改。所以，请注意这一点。有关更多详细信息，请参阅以下博客：http://www.cloudidentity.com/blog/2014/10/30/adal-net-v3-preview-pcl-xamarin-support/

<h3>2015 年 5 月 26 日更新</h3>
由于和 Xamarin 有关，在 ADAL.v3 预览版的 3 月更新中进行了多项改进。 
<ul>
	<li>
		ADAL 移至新的 iOS 版 Xamarin Unified API
	</li>
	<li>
		提供了一个具有易于使用的身份验证的可移植类库
	</li>
</ul>
<p>

注意，这仍然是预览版，不应该用于生产中；通过这些改进可以更轻松地在项目中使用 ADAL。请参考[此处](http://www.cloudidentity.com/blog/2015/03/04/adal-v3-preview-march-refresh/)这篇博客文章以深入了解 3 月的更新，并参考[此处](https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/3.1.203031538-alpha)这篇博客以查看 NuGet 包中针对最新 ADAL 版本所包含的所有内容。

此外，如需了解更多的 Office 365 API，请参阅相应的 GitHub：http://github.com/officedev

我们使用的也是 ADAL 的预览版，你必须配置 MyGet 程序包：

* 转到“解决方案资源管理器”->“[项目节点]”->“管理 NuGet 程序包...”->“设置”。
* 点击右上角的“+”按钮
* 在“名称”字段中，输入类似于“AAD Nightly”的内容
* 在“源”字段中，输入http://www.myget.org/f/azureadwebstacknightly/
* 点击“更新”
* 点击“确定”


### 获取帮助

如果你有任何疑问，只需在 Evolve 2014 上联系

Xamarin 工作人员。此移动应用是由你在 [Xamarin](http://www.xamarin.com/) 的好友带来的。

### 屏幕截图

![显示人员列表的 Android 应用程序屏幕截图](Screenshots/android1.png) ![显示人员详细信息页面的 Android 屏幕截图](Screenshots/android2.png)
![显示人员列表的 iOS 应用程序屏幕截图](Screenshots/ios1.png) ![显示人员详细信息页面的 iOS 屏幕截图](Screenshots/ios2.png)

### 开发者：
- James Montemagno：[Twitter](http://www.twitter.com/jamesmontemagno) | [博客](http://motzcod.es) | [GitHub](http://www.github.com/jamesmontemagno)
