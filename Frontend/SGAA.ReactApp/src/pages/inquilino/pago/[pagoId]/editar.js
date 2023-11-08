import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPago, abonarPago } from '/src/api/inquilino';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PagoEditarForm } from '/src/sections/pago/pago-editar-form';


const Page = () => {
  const router = useRouter();
  const [pago, setPago] = useState(null);
  const pagoId = router.query.pagoId;

  const getBreadcrumbsConfig = (pagoId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/inquilino/pago', title: 'Pagos' },
      { url: '/inquilino/pago/' + pagoId + '/editar', title: pagoId }
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
      form={pago && <PagoEditarForm pago={pago} handleSubmit={abonarPago} handleConfirmationChange={() => router.push('/inquilino/pago')} />}
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
