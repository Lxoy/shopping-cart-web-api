import type { CartItem } from "../types/CartItem"
import './CartItemListElement.css';

type Props = {
    cartItem: CartItem;
    onDelete: (articleId: number) => void;
};

export default function CartItem({ cartItem, onDelete }: Props) {
    return (
        <div className="cart-item">
            <div className="article-info">
                <span className="article-name">{cartItem.name}</span>
                <span className="article-price">€{cartItem.price}</span>
                <span>Qty: {cartItem.quantity}</span>
            </div>

            <button
                    className="delete-button"
                    onClick={() => onDelete(cartItem.articleId)}
                >
                    Remove
                </button>
        </div>
    );
}
