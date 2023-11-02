import { useRouter } from 'next/router';
import { registrarUnidad } from '/src/api/propietario';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadCrearForm } from '/src/sections/unidad/unidad-crear-form';

const Page = () => {
  const router = useRouter();

  const onUnidadCreated = (result) => {
    if (result)
      router.push('/propietario/unidad');
  };

  const breadcrumbsConfig = [
    { url: '/', title: 'Inicio' },
    { url: '/propietario/unidad', title: 'Unidades' },
    { url: '/propietario/unidad/crear', title: 'Crear' }
  ];

  return (
    <FancyFormPage
      roles={['Propietario']}
      form={<UnidadCrearForm handleSubmit={registrarUnidad} handleConfirmationChange={onUnidadCreated} includeAdminRol={true} />}
      title={'Crear Unidad'}
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
