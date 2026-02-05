import type { CartItem } from "../types/CartItem"
import './CartItemListElement.css';

type Props = {
    cartItem: CartItem;
    onAdd: (id: number) => void;
    onDelete: (id: number) => void;
};

export default function CartItem({ cartItem, onAdd, onDelete }: Props) {
    return (
        <div className="cart-item">
            <div className="article-info">
                <span className="article-name">{cartItem.name}</span>
                <span className="article-price">€{cartItem.price}</span>
                <span>Qty: {cartItem.quantity}</span>
            </div>

            <div>
                <button
                    className="add-button"
                    onClick={() => onAdd(cartItem.articleId)}
                >
                    +
                </button>

                <button
                    className="delete-button"
                    onClick={() => onDelete(cartItem.articleId)}
                >
                    -
                </button>
            </div>
        </div>
    );
}
