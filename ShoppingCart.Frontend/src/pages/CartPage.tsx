import { useEffect, useState } from "react"
import { getCart } from "../api/cartApi";
import type { Cart } from "../types/Cart";
import './CartPage.css';
import CartItemList from "../components/CartItemsList";

export default function CartPage() {
    const [cart, setCart] = useState<Cart>();
    const [loading, setLoading] = useState(true);
    const userId = 1;

    const loadCart = async () => {
        try {
            const data = await getCart(userId);
            setCart(data);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        loadCart()

    }, []);

    if (loading) return <p>Loading...</p>;

    if (!cart) return <p>Cart is empty</p>;

    return <div className="cart-page">
        <a href="/articles" className="article-nav">Articles</a>
        <div>
            <h2>Your Cart</h2>
                    <div className="cart-list">
            <CartItemList
                cartItems={cart!.items}
                onAdd={() => { }}
                onDelete={() => { }} />
        </div>
        </div>
    </div>
}