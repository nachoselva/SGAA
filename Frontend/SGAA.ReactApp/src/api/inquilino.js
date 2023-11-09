import { handleFetch } from '/src/api/fetcher';

const getAuthorizationHeader = () => 'Bearer ' + window.localStorage.getItem('jwt');

export const getAplicaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion', requestOptions);
}

export const getAplicacion = (aplicacionId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion/' + aplicacionId, requestOptions);
}

export const getAplicacionActive = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion/active', requestOptions);
}

export const registrarAplicacion = (aplicacion) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(aplicacion)
  };
  return handleFetch('/inquilino/aplicacion', requestOptions);
}

export const actualizarAplicacion = (aplicacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(aplicacion)
  };
  return handleFetch('/inquilino/aplicacion/' + aplicacion.id, requestOptions);
}

export const getPostulaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/postulacion', requestOptions);
}

export const getPostulacion = (id) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/postulacion/' + id, requestOptions);
}

export const crearPostulacion = (postulacion) => {
  const requestOptions = {
    method: 'Post',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(postulacion)
  };
  return handleFetch('/inquilino/postulacion', requestOptions);
}

export const cancelarPostulacion = (postulacionId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({})
  };
  return handleFetch('/inquilino/postulacion/' + postulacionId + '/cancelar', requestOptions);
}

export const aceptarOferta = (postulacionId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({ fechaDesde: null, fechaHasta: null })
  };
  return handleFetch('/inquilino/postulacion/' + postulacionId + '/oferta/aceptar', requestOptions);
}

export const rechazarOferta = (postulacionId) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({})
  };
  return handleFetch('/inquilino/postulacion/' + postulacionId + '/oferta/rechazar', requestOptions);
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
  return handleFetch('/inquilino/publicacion/' + publicacionId, requestOptions);
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
  return handleFetch('/inquilino/pago', requestOptions);
}

export const getPagosByContrato = (contratoId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/pago/contrato/' + contratoId, requestOptions);
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
  return handleFetch('/inquilino/pago/' + id, requestOptions);
}

export const abonarPago = (pago) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(pago)
  };
  return handleFetch('/inquilino/pago/' + pago.id+'/abonar', requestOptions);
}

