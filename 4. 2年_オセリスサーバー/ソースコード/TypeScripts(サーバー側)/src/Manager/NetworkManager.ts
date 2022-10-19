import {Socket} from "net";
import PlayerManager from "../Player/PlayerManager";

export default class NetworkManager
{
    // ルーム内のプレイヤー情報
    // 本来はRoomクラスとかで個々に持たせる
    public static _playerManager:PlayerManager = new PlayerManager();

    /**
     *  指定ソケットにデータを送信する
     *  @param socket 送信先(相手)
     *  @param buffer データ
     */
    public static SendMessage(buffer, socket:Socket)
    {
        // 引数の渡されたbuffer(データ)をstring化
        let message = JSON.stringify(buffer);

        // パケットが結合してもクライアント側でSplitできるように、末尾に分割用記号を追記
        message += "$";

        // 送信
        socket.write(message);
    }

    /**
     * 接続しているPlayer全員にメッセージをおくる関数
     * @param buffer データ
     */
    public static BroadcastMessage(buffer)
    {
        buffer.id = "";

        // for of文で_activePlayerの数にメッセージ送信(Send)をおこなう
        for (const player of this._playerManager._activePlayer) {
            this.SendMessage(buffer, player.socket);
        }
    }

    /**
     * 自分以外の相手にMessageを送る関数
     * @param buffer 送信先
     * @param id 自分のid
     * @constructor
     */
    public static OpponentSendMessage(buffer, id:string)
    {
        // 相手のSocketを入れる変数
        let opponentSocket:Socket = null;

        // for of文で、一つ一つidを見て自分とは違う相手を判別
        for (const activePlayer of this._playerManager._activePlayer) {
            if (activePlayer.id !== id)
            {
                // 代入
                opponentSocket = activePlayer.socket;
                break;
            }
        }

        // エラーのキャッチ
        if (opponentSocket === null) {
            console.log(`プレイヤー (${id}) に対する相手が見つかりませんでした`);
            return;
        }

        // 自分から見た相手の情報を送信しないように中身を空にする
        buffer.id = "";

        // 送信
        this.SendMessage(buffer, opponentSocket);
    }
}