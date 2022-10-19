import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";

export default function (data, socket:Socket) {
    // PlayerManagerのactivePlayersから、プレイヤーIDの一致するやつを削除する
    NetworkManager._playerManager._activePlayer = NetworkManager._playerManager._activePlayer.filter(x => x.id !== data.id);

    console.log(`プレイヤー (${data.id}) が切断しました`);

    // 相手にも通知するbool変数がtrueなら相手にも通知する
    if (data.isNotifyOpponent === true) {
        const req = {
            _packetType: "OpponentDisconnect",
        }

        NetworkManager.BroadcastMessage(req);
    }
}