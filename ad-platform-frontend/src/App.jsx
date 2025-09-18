import { useState } from "react";
import api from "./api/axios";

function App() {
  const [file, setFile] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");
  const [searchResult, setSearchResult] = useState(null);
  const [message, setMessage] = useState("");
  // File upload
  const handleUpload = async () => {
    if (!file) {
      setMessage("Please select a file.");
      return;
    }
  // Search
    const formData = new FormData();
    formData.append("file", file);

    try {
      const res = await api.post("/ads/upload", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      setMessage(res.data.message);
    } catch (err) {
      console.error(err);
      setMessage("An error occurred while uploading the file.");
    }
  };

  const handleSearch = async () => {
    if (!searchQuery) {
      setMessage("Please enter a search term.");
      return;
    }

    try {
      const res = await api.get("/ads/search", {
        params: { query: searchQuery },
      });
      setSearchResult(res.data.result);
      setMessage("");
    } catch (err) {
      console.error(err);
      setMessage("An error occurred while searching.");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Ad Platform</h1>

      {/* Uploading File */}
      <div style={{ marginBottom: "20px" }}>
        <h2>Upload File</h2>
        <input type="file" onChange={(e) => setFile(e.target.files[0])} />
        <button onClick={handleUpload}>Upload</button>
      </div>

      {/* Search */}
      <div style={{ marginBottom: "20px" }}>
        <h2>Search</h2>
        <input
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />
        <button onClick={handleSearch}>Search</button>
      </div>

      {/* Results */}
      {message && <p style={{ color: "blue" }}>{message}</p>}
      {searchResult && (
        <ul>
          {searchResult.map((line, i) => (
            <li key={i}>{line}</li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default App;
