import { useRouter } from 'next/router';
import { registrarUnidad } from '/src/api/propietario';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { useAuthContext } from '/src/contexts/auth-context';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadCrearForm } from '/src/sections/unidad/unidad-crear-form';

const Page = () => {
  const router = useRouter();
  const { user } = useAuthContext();

  const onUnidadCreated = (result) => {
    if (result)
      router.push('/propietario/unidad');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/unidad', title: 'Unidades' },
    { url: '/propietario/unidad/crear', title: 'Crear' }
  ];

  return (
    <FancyFormPage
      roles={['Propietario']}
      form={user && <UnidadCrearForm handleSubmit={registrarUnidad} handleConfirmationChange={onUnidadCreated} licencia={user.licencia} />}
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
