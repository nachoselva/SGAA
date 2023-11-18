import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPago } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PagoLeerForm } from '/src/sections/pago/pago-leer-form';


const Page = () => {
  const router = useRouter();
  const [pago, setPago] = useState(null);
  const pagoId = router.query.pagoId;

  const getBreadcrumbsConfig = (pagoId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/pago', title: 'Pagos' },
      { url: '/administrador/pago/' + pagoId, title: pagoId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(pagoId);

  useEffect(() => {
    if (router.isReady)
      getPago(pagoId)
        .then((response) => {
          setPago(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      form={pago && <PagoLeerForm pago={pago} rol='Administrador' />}
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
