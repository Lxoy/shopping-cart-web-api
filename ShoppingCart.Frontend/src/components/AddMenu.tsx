import './AddMenu.css';

type Props = {
    selectedArticleId: number;
    quantity: number;
    setQuantity: (num: number) => void;
    onConfirm: (articleId: number, quantity: number) => Promise<void>;
    onClose: () => void;
};

export default function AddMenu({
    selectedArticleId,
    quantity,
    setQuantity,
    onConfirm,
    onClose }: Props) {
    return <div className="add-box">
        <h3>Add to cart</h3>

        <input
            type="number"
            min={1}
            value={quantity}
            onChange={e => setQuantity(Number(e.target.value))}
        />

        <div className="actions">
            <button
                onClick={async () => {
                    await onConfirm(selectedArticleId, quantity);
                    onClose();
                }}
            >
                Confirm
            </button>

            <button onClick={onClose}>
                Cancel
            </button>
        </div>
    </div>
}