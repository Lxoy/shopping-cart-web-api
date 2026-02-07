import type { CartItem } from "../../types/CartItem"
import './CartItemListElement.css';

type Props = {
    cartItem: CartItem;
    isInvalid: boolean;
    onChange: (id: number, quantity: number) => void
    onDelete: (id: number) => void;
};

export default function CartItem({ cartItem, isInvalid, onChange, onDelete }: Props) {
    const handleIncrease = () => {
        if (!isInvalid) {
            onChange(cartItem.id, cartItem.quantity + 1);
        }
    };

    const handleDecrease = () => {
        if (!isInvalid && cartItem.quantity > 1) {
            onChange(cartItem.id, cartItem.quantity - 1);
        }
    };

    return (
        <div className={`cart-item ${isInvalid ? "invalid" : ""}`}>
            <div className="article-info">
                <span className="article-name">
                    {cartItem.name}
                </span>

                {isInvalid && (
                    <span className="invalid-message">
                        Article no longer available
                    </span>
                )}

                <span className="article-price">Price: €{cartItem.price}</span>
                <span>Qty: {cartItem.quantity}</span>
                <span className="article-price">Total: €{cartItem.total}</span>
            </div>

            <div>
                <button
                    className="add-button"
                    onClick={handleIncrease}
                    disabled={isInvalid}
                >
                    +
                </button>

                <button
                    className="delete-button"
                    onClick={handleDecrease}
                    disabled={isInvalid}
                >
                    -
                </button>

                <button
                    className="delete-button"
                    onClick={() => onDelete(cartItem.id)}
                >
                    Remove
                </button>
            </div>
        </div>
    );
}
