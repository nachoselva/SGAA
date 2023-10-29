const apiUrl = 'https://localhost:44371'

export const handleFetch =
  (path, requestOptions) =>
    fetch(apiUrl + path, requestOptions)
      .then(async (response) => {
        const text = await response.text();
        const json = text ? JSON.parse(text) : {};
        if (response.status == 200) {
          return json;
        } else {
          throw { statusCode: response.status, body: json };
        }
      })
      .catch((err) => {
        throw err;
      });