import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { cancelarContrato, getContrato } from '/src/api/administrador';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { ContratoCancelarForm } from '/src/sections/contrato/contrato-cancelar-form';

const Page = () => {
  const router = useRouter();
  const [contrato, setContrato] = useState(null);
  const contratoId = router.query.contratoId;

  const onContratoCancelado = (result) => {
    if (result)
      router.push('/administrador/contrato');
  };

  const getBreadcrumbsConfig = (contratoId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/contrato', title: 'Contratos' },
      { url: '/administrador/contrato/' + contratoId, title: contratoId },
      { url: '/administrador/contrato/' + contratoId+'/cancelar', title: 'Cancelar Contrato' },
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(contratoId);

  useEffect(() => {
    if (router.isReady)
      getContrato(contratoId)
        .then((response) => {
          setContrato(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={contrato && <ContratoCancelarForm contrato={contrato} handleSubmit={cancelarContrato} handleConfirmationChange={onContratoCancelado} />}
      title={'Cancelar Contrato'}
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
