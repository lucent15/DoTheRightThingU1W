# DoTheRightThingU1W

ゲームはこちら：https://unityroom.com/games/dotherightthing

ゲームジャムのお題「正」の時に開発したゲームです。ゲーム自体は完成していたものの、ビルドエラーが解決できなくて当時は期間中の投稿ができませんでした。お題を満たす過程でごく簡素なシンプルな見下ろし型のRTSになりましたが、減少量が分かるHPの表示や、スローモーションを活用して複数キャラをリアルタイムで同時に操作する事を試してみたくて開発しました。

AlliesController.cs：味方キャラの操作  
BaseStatus.cs：共通  
Bullet.cs：味方、敵が発射する弾  
CivController.cs：一般市民の制御  
ClearSceneScript.cs：クリアシーンの制御  
EffectGenerator.cs：死亡アイコンの表示  
EnemyController.cs：敵キャラクターの制御  
FadeInOutScript.cs：フェードイン/アウトの制御  
GameDirector.cs：ゲームの進行管理  
HPScript.cs：敵味方のHP処理に関するスクリプト  
MenuDirector.cs：味方メニュー操作時のスローモーション  
Score.cs：キル数やボリュームの値保存  
UIController_Overlay.cs：選択中の味方のハイライト  
