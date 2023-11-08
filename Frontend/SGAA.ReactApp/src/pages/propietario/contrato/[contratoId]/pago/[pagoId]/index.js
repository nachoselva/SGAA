import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPago } from '/src/api/propietario';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PagoLeerForm } from '/src/sections/pago/pago-leer-form';


const Page = () => {
  const router = useRouter();
  const [pago, setPago] = useState(null);
  const pagoId = router.query.pagoId;
  const contratoId = router.query.contratoId;

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/contrato', title: 'Contratos' },
    { url: '/propietario/contrato/' + contratoId, title: contratoId },
    { url: '/propietario/contrato/' + contratoId + '/pago', title: 'Pagos' },
    { url: '/propietario/contrato/' + contratoId + '/pago/' + pagoId, title: pagoId }
  ];

  useEffect(() => {
    if (router.isReady)
      getPago(pagoId)
        .then((response) => {
          setPago(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      form={pago && <PagoLeerForm pago={pago} rol='Propietario' />}
      title={'Detalle Pago'}
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
