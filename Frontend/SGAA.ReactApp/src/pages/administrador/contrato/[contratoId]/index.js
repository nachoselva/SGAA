import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getContrato } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { ContratoLeerForm } from '/src/sections/contrato/contrato-leer-form';

const Page = () => {
  const router = useRouter();
  const [contrato, setContrato] = useState(null);
  const contratoId = router.query.contratoId;

  const getBreadcrumbsConfig = (contratoId) =>
    [
      { url: '/', title: 'Inicio' },
      { url: '/administrador/contrato', title: 'Contratos' },
      { url: '/administrador/contrato/' + contratoId, title: contratoId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(contratoId);

  useEffect(() => {
    getContrato(contratoId)
      .then((response) => {
        setContrato(response);
      });
  }, []);

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={contrato && <ContratoLeerForm contrato={contrato} />}
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
