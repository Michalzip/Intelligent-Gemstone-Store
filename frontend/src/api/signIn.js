const SignIn = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [token, setToken] = useState("");
  const [error, setError] = useState("");

  const handleLogin = async () => {
    try {
      const response = await fetch("adres_api/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        throw new Error("Błąd logowania. Sprawdź nazwę użytkownika i hasło.");
      }

      const data = await response.json();
      const receivedToken = data.token;
      setToken(receivedToken);

      localStorage.setItem("jwtToken", receivedToken);
    } catch (error) {
      setError(error.message);
      console.error("Błąd logowania:", error);
    }
  };

  return (
    <div>
      {token ? (
        <p>Zalogowano! Twój token JWT: {token}</p>
      ) : (
        <form>
          <input
            type="text"
            placeholder="Nazwa użytkownika"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="button" onClick={handleLogin}>
            Zaloguj się
          </button>
          {error && <p>{error}</p>}
        </form>
      )}
    </div>
  );
};

export default SignIn;
