// ファイルインポートする際に、やり方が複数あり
//  * = ファイルの全部
// {} = 要素の一つをインポートする (, で複数インポートも可)
import  * as net from "net";
import {connect} from "net";
import distribute from "./src/Events/DistributeEvent"

const server = net.createServer(socket => {
    // dataの内容, distributeを呼ぶ(ここで通信の仕分け)
    socket.on("data", (buffer) => distribute.instance.Distribution(buffer, socket));
});

server.listen(3359);
