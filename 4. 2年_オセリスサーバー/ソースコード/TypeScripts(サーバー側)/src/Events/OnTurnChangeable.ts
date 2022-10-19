import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";

export default function (data, socket:Socket) {

    // IDからプレイヤー判別
    const player = NetworkManager._playerManager.GetPlayerById(data.id);
    console.log(data.id);
    // プレイヤー自身がもっているcanChangeTurnをtrueにする
    player.canChangeTurn = true;

    // プレイヤー全員がcanChangeTurn(ターンの遷移可能かどうかのチェック)
    if (NetworkManager._playerManager._activePlayer.every(value => value.canChangeTurn))
    {
        // 送信を行う
        NetworkManager.BroadcastMessage(data);
        // 送信が終わったら
        for (let i = 0; NetworkManager._playerManager._activePlayer.length > i; i++) {
            NetworkManager._playerManager._activePlayer[i].canChangeTurn = false;
        }
    }
}