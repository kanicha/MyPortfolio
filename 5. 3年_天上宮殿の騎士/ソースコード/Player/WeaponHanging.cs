using UnityEngine;

/// <summary>
/// ぶら下がり状態の処理を管理する WeaponThrowing の partialClass
/// </summary>
public partial class WeaponThrowing
{
    [SerializeField, Tooltip("どれくらい剣にたいしてプレイヤーを着地させるか")]
    private Vector3 _compensationPostion;

    /// <summary>
    /// ぶら下がり状態の処理関数
    /// </summary>
    private void WeaponHanging(Vector3 rayHitPoint, PlayerActionInfo actionInfo)
    {
        // Hangingの関数が通った時点でどっちか判別してステートをつけ
        // すでに片方でぶら下がり状態だった場合はフラグとステートを解除する
        switch (actionInfo.actHand)
        {
            case PlayerInputEvent.PlayerHand.Left:
                if (PlayerStateManager.HasFlag(PlayerStatus.PlayerState.HangingR))
                {
                    ResetSword(PlayerInputEvent.PlayerHand.Right, false);
                    _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingL,
                        PlayerStateChangeOptions.Add, null, null));
                }
                else
                    _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingL,
                        PlayerStateChangeOptions.Add, null, null));
                break;
            case PlayerInputEvent.PlayerHand.Right:
                if (PlayerStateManager.HasFlag(PlayerStatus.PlayerState.HangingL))
                {
                    ResetSword(PlayerInputEvent.PlayerHand.Left, false);
                    _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingR,
                        PlayerStateChangeOptions.Add, null, null));
                }
                else
                    _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingR,
                        PlayerStateChangeOptions.Add, null, null));
                break;
            default:
                break;
        }


        // まず斜面をどう求めていくかを考える必要がありそう
        // まず、斜面がぶら下がり状態の斜面条件を満たしているか
        // if(斜面が足りているか) return;
        // 足りていたら処理を行う

        // hitしたオブジェクトの座標をプレイヤー座標に代入
        _playerObject.transform.position = rayHitPoint + _compensationPostion;

        // ぶら下がり状態なので重力と移動を禁止する
        _playerRb.isKinematic = true;
    }

    /// <summary>
    /// ぶら下がり状態を解除して武器を投げれるように修正を行う関数 (ステートで解除タイプ)
    /// </summary>
    public void ResetSword()
    {
        _playerRb.isKinematic = false;

        // つかみ状態でステートがついているのでステートを見てリセット処理
        if (PlayerStateManager.HasFlag(PlayerStatus.PlayerState.HangingL))
        {
            _leftWeapon.IsThrowing = false;
            SwordPositionReset(PlayerInputEvent.PlayerHand.Left);
            // ParentConstraintも合わせて初期化
            _broker.Publish(PlayerEvent.OnParentChangeToObject.GetEvent(null, new PlayerActionInfo()
            {
                actHand = PlayerInputEvent.PlayerHand.Left
            }));
            _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingL,
                PlayerStateChangeOptions.Delete, null, null));
        }
        else if (PlayerStateManager.HasFlag(PlayerStatus.PlayerState.HangingR))
        {
            _rightWeapon.IsThrowing = false;
            SwordPositionReset(PlayerInputEvent.PlayerHand.Right);
            // ParentConstraintも合わせて初期化
            _broker.Publish(PlayerEvent.OnParentChangeToObject.GetEvent(null, new PlayerActionInfo()
            {
                actHand = PlayerInputEvent.PlayerHand.Right
            }));
            _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingR,
                PlayerStateChangeOptions.Delete, null, null));
        }
    }

    /// <summary>
    /// ぶら下がり状態を解除して武器を投げれるように修正を行う関数 (actionInfoで解除タイプ)
    /// </summary>
    /// <param name="actionInfo"> 右手か左手かを判別する関数 </param>>
    /// <param name="releaseKinematic"> true:kinematicの解除を行う false: 行わない </param>>
    public void ResetSword(PlayerInputEvent.PlayerHand actionInfo, bool releaseKinematic)
    {
        if (releaseKinematic)
            _playerRb.isKinematic = false;

        // つかみ状態でステートがついているのでステートを見てリセット処理
        if (actionInfo == PlayerInputEvent.PlayerHand.Left)
        {
            _leftWeapon.IsThrowing = false;
            SwordPositionReset(PlayerInputEvent.PlayerHand.Left);
            // ParentConstraintも合わせて初期化
            _broker.Publish(PlayerEvent.OnParentChangeToObject.GetEvent(null, new PlayerActionInfo()
            {
                actHand = PlayerInputEvent.PlayerHand.Left
            }));
            _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingL,
                PlayerStateChangeOptions.Delete, null, null));
        }
        else if (actionInfo == PlayerInputEvent.PlayerHand.Right)
        {
            _rightWeapon.IsThrowing = false;
            SwordPositionReset(PlayerInputEvent.PlayerHand.Right);
            // ParentConstraintも合わせて初期化
            _broker.Publish(PlayerEvent.OnParentChangeToObject.GetEvent(null, new PlayerActionInfo()
            {
                actHand = PlayerInputEvent.PlayerHand.Right
            }));
            _broker.Publish(PlayerEvent.OnStateChangeRequest.GetEvent(PlayerStatus.PlayerState.HangingR,
                PlayerStateChangeOptions.Delete, null, null));
        }
    }
}
