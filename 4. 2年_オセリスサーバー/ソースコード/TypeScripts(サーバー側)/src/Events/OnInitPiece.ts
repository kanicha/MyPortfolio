import {Socket} from "net";
import NetworkManager from "../Manager/NetworkManager";

export default function (data, socket:Socket)
{
    NetworkManager.OpponentSendMessage(data, data.id);
}
