# SoraCommonNet

## なにこれ

.NET環境から[SORACOM](https://dev.soracom.io/jp/)のAPIへとアクセスする非公式SDKです。
`Operator`としてログインしたり、自身に紐付いている`Subscriber`(SIM)の一覧とその詳細情報を取得したりできます。

これを使うと、たとえば

 * Microsoft Azure環境でSORACOM Beamを受けてSIM認証をおこなったり
 * モバイル向けのSORACOM管理コンソールをC#で書いたり
 * SORACOM上のSIM一覧とその状態をUnityでビジュアライズしたり変更したり

ということをできます。

2015年10月時点では公式の.NET SDKが用意されていなかったのと、本家のREST APIをそのままC#へとマッピングしても使い勝手がイマイチそうだったので作成しました。

## インストール方法

バイナリは[NuGetパッケージ](https://www.nuget.org/packages/SoraCommonNet/)で公開しています。

```
Install-Package SoraCommonNet
```

として導入してください。

## 簡単な使い方
```
var op = await Operator.Authenticate("foo@example.com", "password");
var subscr = await op.RetrieveSubscriber("111122223333X");
Debug.WriteLine("IMSI: " + subscr.Imsi);
```

## 使い方

リファレンスはまだ用意していません。

基本的にIntelliSenseで眺めれば気持ちで把握できるAPIになっていると思いますが、取っ掛かりとしては[テストコード(一部コメントアウトされている)](https://github.com/muojp/SoraCommonNet/blob/master/SoraCommonTest/Program.cs#L28)を眺めてみてください。

## できること

 * `Operator`の新規登録(仮登録メール送信〜登録確認まで)
 * `Operator`としてのログイン
 * トークン更新
 * `Subscriber`(SIM)の一覧取得
 * `Subscriber`(SIM)のアクティブ化/非アクティブ化
 * `Subscriber`(SIM)の廃止プロテクト(TP)制御

## (まだ)できないこと

 * `Subscriber`(SIM)の絞込検索
 * グループ管理
 * イベント管理

## (まだ)動作を確認できていないもの

 * `Operator`のパスワード変更
 * `Subscriber`(SIM)の廃止

## 非公式SORACOM SDK開発者向け: 自動テスト用の非公式sandbox

公式のSORACOM APIはsandboxを持たないため、SoraCommonNetの自動テストは困難です。
ひたすらローカル完結のモックテストをするのも少々不毛な気がしました。

このため、SORACOM APIの応答を模倣したsandbox(というかモック)サービスを作りました。

ソースコードは[SoraCommonNetSandbox](https://github.com/muojp/SoraCommonNet/tree/master/SoraCommonNetSandbox)以下にあります。適宜本リポジトリをcloneしてよしなにサービスを立ち上げてください。

Azure Web Appとして構築したsandboxを置いておきますので、面倒なら[sandbox](http://soracommonnetsandbox.azurewebsites.net/)を叩いていただいても構いません(Azure Web Appの無料枠で動作させていますが、利用制限の上限へ達することはないはずです)。


## LICENSE

Apache 2.0
