import { handleFetch } from '/src/api/fetcher';

const getAuthorizationHeader = () => 'Bearer ' + window.localStorage.getItem('jwt');

export const getUnidades = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/unidad', requestOptions);
}

export const getUnidad = (unidadId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/unidad/' + unidadId, requestOptions);
}

export const getPublicaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/publicacion', requestOptions);
}

export const getPublicacion = (publicacionId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/publicacion/' + publicacionId, requestOptions);
}

export const registrarUnidad = (unidad) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(unidad)
  };
  return handleFetch('/propietario/unidad', requestOptions);
}

export const actualizarUnidad = (unidad) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(unidad)
  };
  return handleFetch('/propietario/unidad/'+unidad.id, requestOptions);
}

export const cancelarPublicacion = (publicacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: {}
  };
  return handleFetch('/propietario/publicacion/' + publicacion.id + '/cancelar', requestOptions);
}

export const cerrarPublicacion = (publicacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: {}
  };
  return handleFetch('/propietario/publicacion/' + publicacion.id + '/cerrar', requestOptions);
}

export const registrarPublicacion = (publicacion) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(publicacion)
  };
  return handleFetch('/propietario/publicacion', requestOptions);
}