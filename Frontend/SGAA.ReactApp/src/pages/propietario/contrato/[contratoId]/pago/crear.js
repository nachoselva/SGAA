import { useRouter } from 'next/router';
import { registrarPago } from '/src/api/propietario';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PagoCrearForm } from '/src/sections/pago/pago-crear-form';

const Page = () => {
  const router = useRouter();
  const contratoId = router.query.contratoId;

  const onPagoCreated = (result) => {
    if (result)
      router.push('/propietario/contrato/' + contratoId + '/pago');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/contrato', title: 'Contratos' },
    { url: '/propietario/contrato/' + contratoId, title: contratoId },
    { url: '/propietario/contrato/' + contratoId + '/pago', title: 'Pagos' },
    { url: '/propietario/contrato/' + contratoId + '/pago/crear', title: 'Crear' }
  ];

  return (
    <>
      {
        contratoId &&
        <FancyFormPage
          roles={['Propietario']}
          form={<PagoCrearForm handleSubmit={registrarPago} handleConfirmationChange={onPagoCreated} contratoId={contratoId} />}
          title={'Crear Pago'}
          breadcrumbsConfig={breadcrumbsConfig}>
        </FancyFormPage>
      }
    </>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
