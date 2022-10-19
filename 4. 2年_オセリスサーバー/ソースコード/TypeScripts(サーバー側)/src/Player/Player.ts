import {Socket} from "net";
import {v4} from "uuid";

export default class Player
{
    public readonly id:string;
    public readonly socket:Socket;
    public isCharaConfirm:boolean = false;
    public canChangeTurn:boolean = false;

    public constructor(socket:Socket) {
        this.id = v4();
        this.socket = socket;
    }
}