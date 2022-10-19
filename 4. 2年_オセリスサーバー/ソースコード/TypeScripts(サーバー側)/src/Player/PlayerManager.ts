import Player from "./Player";

export default class PlayerManager
{
    public _activePlayer:Player[] = [];

    /**
     * IDでプレイヤーのインスタンス取得
     * @param id プレイヤーid
     * @return プレイヤーのインスタンス
     */
    public GetPlayerById(id:string): Player
    {
        // for of文で、一つ一つidを見る
        for (const activePlayer of this._activePlayer) {
            if (activePlayer.id === id)
            {
                return activePlayer;
            }
        }

        return null;
    }

    /**
     * プレイヤー全員がキャラクターを確定しているかどうか判別する関数
     */
    public CompleteCharaConfirm(): boolean
    {
        // 全員のプレイヤーが確定しているかどうか判別
        let playerConfirm:boolean = true;

        for (const activePlayer of this._activePlayer) {
            if (activePlayer.isCharaConfirm === false){
                playerConfirm = false;
            }
        }

        // こっちでも全部trueか確認できる
        // this._activePlayer.every(value => value.isCharaConfirm);

        return playerConfirm;
    }
}