import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";

export default function (data, socket:Socket) {
    /*console.log("x座標" + data.piecePos.x);
    console.log("z座標" + data.piecePos.z);
    console.log("y座標" + data.piecePos.y);*/

    console.log(JSON.stringify(data));
    NetworkManager.OpponentSendMessage(data, data.id);
}