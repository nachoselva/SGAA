export const convertFileToBase64 =
  (file) =>
    new Promise((resolve) => {
      const reader = new FileReader();
      reader.onload = function () {
        resolve({
          name: file.name,
          type: file.type,
          size: Math.round(file.size / 1024),
          base64: reader.result
        });
      }
      if (file)
        reader.readAsDataURL(file);
    });