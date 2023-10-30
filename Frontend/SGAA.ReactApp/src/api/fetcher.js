import { startLoading, endLoading } from "/src/slices/loading-slice";

const apiUrl = 'https://localhost:44371'

export const handleFetch =
  (path, requestOptions) => {
    startLoading();
    return fetch(apiUrl + path, requestOptions)
      .then(async (response) => {
        const text = await response.text();
        const json = text ? JSON.parse(text) : {};
        if (response.status != 200) {
          endLoading();
          throw { statusCode: response.status, body: json };
        }
        endLoading();
        return json;
      })
      .catch((err) => {
        endLoading()
        throw err;
      });
  }
