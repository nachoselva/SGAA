export const login =
  async (email, password) => {
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email, password: password })
    };
    return await fetch(`https://localhost:44371/security/login`, requestOptions);
  }