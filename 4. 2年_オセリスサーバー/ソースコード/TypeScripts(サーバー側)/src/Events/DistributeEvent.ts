import {Buffer} from "buffer";
import {Socket} from "net";

export default class DistributeEvent
{
    private static _instance: DistributeEvent;

    // コンストラクタ
    private constructor() {
    }

    public static get instance()
    {
        if (this._instance == null)
        {
            this._instance = new DistributeEvent();
        }

        return this._instance;
    }

    // 通信ごとに分配をおこなう関数
    public async Distribution(buffer: Buffer, socket: Socket)
    {
        // 取得してきたデータをJson化
        const jsonMessage = JSON.parse(buffer.toString());

        // イベントの名前変数を用意 (文字列の中にファイル名(通信名))
        const eventName = `./On${jsonMessage._packetType}`;
        const event = await import(eventName).catch(reason => console.log(`関数「${eventName}」はありません`));

        console.log(`パケット「${jsonMessage._packetType}」を受け取りました`);

        if (event)
        {
            event.default(jsonMessage, socket);
        }
    }
}