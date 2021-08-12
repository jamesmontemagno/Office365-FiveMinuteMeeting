5 分間の会議 - Office 365 および Xamarin のサンプル
=========================

Office 365 Contact ブラウザー、エディター、カレンダーへの機能会議、連絡先も整理します。

詳細については、Ignite 2015 の Office 365 セッションをご覧ください: http://channel9.msdn.com/Events/Ignite/2015/BRK3342

## :警告
このサンプルは、ADAL のプレビュー エディションを使用しており、将来的に重大な変更が加えられる可能性があることが確認されています。したがって、その点にご注意ください。詳細については、このブログを参照してください: http：//www.cloudidentity.com/blog/2014/10/30/adal-net-v3-preview-pcl-xamarin-support/

<h3>2015 年 5 月 26 日の最新情報</h3>
ADAL.v3 プレビューの 3 月の更新により、Xamarin に関連するいくつかの改善が行われました。 
<ul>
	<li>
		ADAL は iOS 向けの新しい Xamarin Unified API に移行します
	</li>
	<li>
		使いやすい認証を備えたポータブル クラス ライブラリを提供します
	</li>
</ul>
<p>

これはまだプレビューにあり、本番環境では使用しないでください。これらの改善により、プロジェクトで ADAL を操作しやすくなります。3 月の更新の詳細については、このブログ投稿 [こちら] (http://www.cloudidentity.com/blog/2015/03/04/adal-v3-preview-march-refresh/) を参照し、最新の ADAL ビルドの NuGet パッケージに含まれているすべてを確認するには、このブログ [こちら] (https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/3.1.203031538-alpha) を参照してください。

さらに、その他の Office 365 API については、GitHub: http://github.com/officedev を参照してください。

ADAL のプレビュー リリースも使用しており、MyGet パッケージを構成する必要があります:

* ソリューション エクスプローラー -> [プロジェクト ノード] -> NuGet パッケージの管理… -> 設定に移動します。
* 右上隅の「+」ボタンを押します
* [名前 ]フィールドに、「AAD Nightly」の効果を入力します
* [ソース] フィールドに、http://www.myget.org/f/azureadwebstacknightly/ と入力します
* [更新] をクリックします
* [OK] をクリックします


### サポートを得る:

ご質問がある場合は、Evolve 2014 で Xamarin のスタッフにお問い合わせください。

このモバイル アプリは、[Xamarin] (http://www.xamarin.com/) において友人により提供されています。

### スクリーンショット

![人のリストを表示するアプリケーションの Android スクリーンショット] (スクリーンショット/android1.png) ![人の詳細ページの Android スクリーンショット] (スクリーンショット/android2.png)
![人のリストを表示するアプリケーションの iOS スクリーンショット] (スクリーンショット/ios1.png) ![人の詳細ページの iOS スクリーンショット] (スクリーンショット/ios2.png)

### 開発者:
- James Montemagno: [Twitter] (http://www.twitter.com/jamesmontemagno) | [ブログ] (http://motzcod.es) | [GitHub] (http://www.github.com/jamesmontemagno)
