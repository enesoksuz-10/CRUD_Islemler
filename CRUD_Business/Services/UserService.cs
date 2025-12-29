using AutoMapper;
using CRUD_Business.Interfaces;
using CRUD_Contracts.Users;
using CRUD_DataAccess.Repositories.Implementations;
using CRUD_Infrastracture.ExceptionHandling.Exceptions;
using CRUD_Model.Entities;

namespace CRUD_Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Repository + AutoMapper DI ile alınır
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            // İsim boş olamaz
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException("İsim alanı boş geçilemez.");

            // Soyisim boş olamaz
            if (string.IsNullOrWhiteSpace(dto.Surname))
                throw new BusinessException("Soyisim alanı boş geçilemez.");

            // İsim uzunluğu
            if (dto.Name.Length > 50)
                throw new BusinessException("İsim 50 karakterden uzun olamaz.");

            // Soyisim uzunluğu
            if (dto.Surname.Length > 50)
                throw new BusinessException("Soyisim 50 karakterden uzun olamaz.");

            // Telefon boş olamaz
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new BusinessException("Telefon numarası alanı boş geçilemez.");

            // Telefon sadece rakam olmalı
            if (!dto.PhoneNumber.All(char.IsDigit))
                throw new BusinessException("Telefon numarası sadece rakamlardan oluşmalıdır.");

            // Telefon uzunluğu (10 hane)
            if (dto.PhoneNumber.Length != 10)
                throw new BusinessException("Telefon numarası 10 haneli olmalıdır.");

            // Telefon 0 ile başlayamaz
            if (dto.PhoneNumber.StartsWith("0"))
                throw new BusinessException("Telefon numarası 0 ile başlayamaz.");

            // TCKN boş olamaz
            if (string.IsNullOrWhiteSpace(dto.TCKN))
                throw new BusinessException("TCKN alanı boş geçilemez.");

            // TCKN sadece rakam olmalı
            if (!dto.TCKN.All(char.IsDigit))
                throw new BusinessException("TCKN sadece rakamlardan oluşmalıdır.");

            // TCKN uzunluğu (11 hane)
            if (dto.TCKN.Length != 11)
                throw new BusinessException("TCKN 11 haneli olmalıdır.");

            // DTO → Entity dönüşümü AutoMapper ile
            var user = _mapper.Map<User>(dto);

            user.RecordStatus = 10;

            // Veritabanına ekleme
            await _userRepository.AddAsync(user);

            // Oluşan Id
            return user.Id;
        }

        // Tüm kullanıcılar
        public async Task<List<ReadUserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Entity → DTO dönüşümü
            return _mapper.Map<List<ReadUserDto>>(users);
        }

        // Id’ye göre kullanıcı
        public async Task<ReadUserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new BusinessException("Kullanıcı bulunamadı.");

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            // Güncellenecek kullanıcı var mı?
            var user = await _userRepository.GetByIdForUpdateAsync(id);

            if (user == null)
                throw new BusinessException("Güncellenecek kullanıcı bulunamadı.");

            // Validation (Create ile aynı kurallar)
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException("İsim alanı boş geçilemez.");

            if (string.IsNullOrWhiteSpace(dto.Surname))
                throw new BusinessException("Soyisim alanı boş geçilemez.");

            if (!dto.PhoneNumber.All(char.IsDigit))
                throw new BusinessException("Telefon numarası sadece rakamlardan oluşmalıdır.");

            if (dto.PhoneNumber.Length != 10)
                throw new BusinessException("Telefon numarası 10 haneli olmalıdır.");

            if (dto.PhoneNumber.StartsWith("0"))
                throw new BusinessException("Telefon numarası 0 ile başlayamaz.");

            if (!dto.TCKN.All(char.IsDigit))
                throw new BusinessException("TCKN sadece rakamlardan oluşmalıdır.");

            if (dto.TCKN.Length != 11)
                throw new BusinessException("TCKN 11 haneli olmalıdır.");

            // DTO → Entity (mevcut entity güncellenir)
            _mapper.Map(dto, user);

            await _userRepository.UpdateAsync(user);
        }
        public async Task SoftDeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdForDeleteAsync(id);

            if (user == null)
                throw new BusinessException("Silinecek kullanıcı bulunamadı.");

            await _userRepository.SoftDeleteAsync(user);
        }

        public async Task ActivateAsync(int id)
        {
            var user = await _userRepository.GetByIdForActivateAsync(id);

            if (user == null)
                throw new BusinessException("Aktif edilecek silinmiş kullanıcı bulunamadı.");

            await _userRepository.ActivateAsync(user);
        }

        public async Task HardDeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdForHardDeleteAsync(id);

            if (user == null)
                throw new BusinessException("Hard delete için öncelikle soft delete yapılmalı.");

            await _userRepository.HardDeleteAsync(user);
        }

    }
}

//-Interface → bağımlılıkları azaltmak için
//- Service → business logic'i ayırmak için
//- async / Task → performans ve ölçeklenebilirlik için
//- private readonly → güvenli ve kontrollü kullanım için
//- Repository çağrısı → DataAccess'i izole etmek için
