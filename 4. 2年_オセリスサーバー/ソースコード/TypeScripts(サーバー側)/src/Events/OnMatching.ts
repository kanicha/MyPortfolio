import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";
import Player from "../Player/Player";

export default async function (data, socket:Socket) {
    const MAX_PLAYER = 2;

    // 満員かどうかを確認
    if (NetworkManager._playerManager._activePlayer.length >= MAX_PLAYER)
    {
        data.isFull = true;
        NetworkManager.SendMessage(data, socket);

        // 満員だったらこれ以上の処理を行わない
        return;
    }

    // 満員じゃなければactivePlayerにsocketを追加
    const player:Player = new Player(socket);
    NetworkManager._playerManager._activePlayer.push(player);

    // サーバーに入れたことをsocketに通知
    data.isJoined = true;
    // 生成したIDを通信の中身に入れる(idはPlayerコンストラクタで生成)
    data.id = player.id;
    data.playerNumber = NetworkManager._playerManager._activePlayer.length;
    NetworkManager.SendMessage(data, socket);

    console.log(`プレイヤー (${player.id}) が参加しました`);

    await new Promise(resolve => setTimeout(resolve, 1000));

    // マッチングしてきた相手が2人目だったら、マッチング完了をブロードキャスト
    if (NetworkManager._playerManager._activePlayer.length == MAX_PLAYER)
    {
        data.id = "";
        data.isJoined = false;
        data.isCompleteMatching = true;
        NetworkManager.BroadcastMessage(data);

        console.log("マッチング完了");
    }
}
