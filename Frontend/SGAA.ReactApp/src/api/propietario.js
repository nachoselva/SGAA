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

export const cancelarPublicacion = (publicacionId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({})
  };
  return handleFetch('/propietario/publicacion/' + publicacionId + '/cancelar', requestOptions);
}

export const cerrarPublicacion = (publicacionId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({})
  };
  return handleFetch('/propietario/publicacion/' + publicacionId + '/cerrar', requestOptions);
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

export const getPagos = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/pago', requestOptions);
}

export const getPagosByContrato = (contratoId) => {
  console.log(contratoId);
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/pago/contrato/'+contratoId, requestOptions);
}

export const getPago = (id) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/propietario/pago/' + id, requestOptions);
}

export const aprobarPago = (pagoId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({})
  };
  return handleFetch('/propietario/pago/' + pagoId+'/aprobar', requestOptions);
}

export const registrarPago = (pago) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(pago)
  };
  return handleFetch('/propietario/pago', requestOptions);
}