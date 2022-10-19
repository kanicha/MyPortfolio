import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";

export default async function (data, socket:Socket)
{
    // IDからプレイヤー判別
    const player = NetworkManager._playerManager.GetPlayerById(data.id);
    console.log(data.id);
    // プレイヤー自身がもっているキャラクターにキャラ確定した情報を代入する
    player.isCharaConfirm = data.isCharaConfirm;

    console.log(`プレイヤー (${player.id}) のキャラ選択が完了しました`);

    await new Promise(resolve => setTimeout(resolve, 1000));

    // 二人確定している状態で分岐
    if (NetworkManager._playerManager.CompleteCharaConfirm())
    {
        // trueにして全員にMessageを送信する
        data.isCompletedConfirm = true;
        NetworkManager.BroadcastMessage(data);

        console.log("全員がキャラ選択完了しました");
    }
}