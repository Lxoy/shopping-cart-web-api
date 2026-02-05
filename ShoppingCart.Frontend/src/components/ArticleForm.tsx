import { useState } from "react";
import { createArticle } from "../api/articleApi";
import './ArticleForm.css'

export default function ArticleForm({
  onCreated
}: {
  onCreated: () => void;
}) {
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();

    await createArticle({
      name,
      price: Number(price)
    });

    setName("");
    setPrice("");
    onCreated();
  };

  return (
    <form onSubmit={submit} className="form">
      <input
        placeholder="Name"
        value={name}
        onChange={e => setName(e.target.value)}
      />

      <input
        placeholder="Price"
        type="number"
        step="0.01"
        value={price}
        onChange={e => setPrice(e.target.value)}
      />

      <button type="submit">Add</button>
    </form>
  );
}
