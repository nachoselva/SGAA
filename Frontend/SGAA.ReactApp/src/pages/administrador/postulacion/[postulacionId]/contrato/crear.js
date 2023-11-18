import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { registrarContrato, getPostulacion } from '/src/api/administrador';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { ContratoCrearForm } from '/src/sections/contrato/contrato-crear-form';

const Page = () => {
  const router = useRouter();
  const postulacionId = router.query.postulacionId;
  const [postulacion, setpostulacion] = useState(null);

  useEffect(() => {
    if (router.isReady)
      getPostulacion(postulacionId)
        .then((response) => {
          setpostulacion(response);
        });
  }, [router.isReady]);

  const onContratoCreated = (result) => {
    if (result)
      router.push('/administrador/contrato');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/postulacion', title: 'Postulaciones' },
    { url: '/administrador/postulacion/' + postulacionId, title: postulacionId },
    { url: '/administrador/postulacion/' + postulacionId + '/contrato/crear', title: 'Crear' }
  ];

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={postulacion && <ContratoCrearForm postulacion={postulacion} handleSubmit={registrarContrato} handleConfirmationChange={onContratoCreated} />}
      entityName={'Contrato'}
      breadcrumbsConfig={breadcrumbsConfig}>
    </FancyFormPage>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
