import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getContrato, renovarContrato } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { ContratoRenovarForm } from '/src/sections/contrato/contrato-renovar-form';

const Page = () => {
  const router = useRouter();
  const [contrato, setContrato] = useState(null);
  const contratoId = router.query.contratoId;

  const onContratoRenovado = (result) => {
    if (result)
      router.push('/administrador/contrato');
  };

  const getBreadcrumbsConfig = (contratoId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/contrato', title: 'Contratos' },
      { url: '/administrador/contrato/' + contratoId, title: contratoId },
      { url: '/administrador/contrato/' + contratoId+'/renovar', title: 'Renovar Contrato' },
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
      form={contrato && <ContratoRenovarForm contrato={contrato} handleSubmit={renovarContrato} handleConfirmationChange={onContratoRenovado} />}
      title={'Renovar Contrato'}
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
