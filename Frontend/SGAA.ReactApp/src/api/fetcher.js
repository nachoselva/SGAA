const apiUrl = 'http://sgaa-prod-docker.eba-fbq3apt5.us-east-1.elasticbeanstalk.com';

export const handleFetch =
  (path, requestOptions) => {
    return fetch(apiUrl + path, requestOptions)
      .then(async (response) => {
        const text = await response.text();
        const json = text ? JSON.parse(text) : {};
        if (response.status != 200) {
          throw { statusCode: response.status, body: json };
        }
        return json;
      })
      .catch((err) => {
        throw err;
      });
  }
