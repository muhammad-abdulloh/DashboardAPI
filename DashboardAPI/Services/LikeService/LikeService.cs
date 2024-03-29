﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardAPI.Models.DTOs.Like;
using DashboardAPI.Models.Exceptions;
using DashboardDBAccess.Data;
using DashboardDBAccess.Exceptions;
using DashboardDBAccess.Repositories.Comment;
using DashboardDBAccess.Repositories.Like;
using DashboardDBAccess.Repositories.Post;
using DashboardDBAccess.Repositories.UnitOfWork;
using DashboardDBAccess.Repositories.User;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;
using FluentValidation;

namespace DashboardAPI.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<ILikeDto> _dtoValidator;

        public LikeService(ILikeRepository repository, IMapper mapper, IUnitOfWork unitOfWork,
            ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, 
            IValidator<ILikeDto> dtoValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _dtoValidator = dtoValidator;
        }

        public async Task<IEnumerable<GetLikeDto>> GetAllLikes()
        {
            return (await _repository.GetAllAsync()).Select(x => _mapper.Map<GetLikeDto>(x)).ToList();
        }

        public async Task<IEnumerable<GetLikeDto>> GetLikes(FilterSpecification<Like> filterSpecification = null, 
            PagingSpecification pagingSpecification = null,
            SortSpecification<Like> sortSpecification = null)
        {
            return (await _repository.GetAsync(filterSpecification, pagingSpecification, sortSpecification)).Select(x => _mapper.Map<GetLikeDto>(x));
        }

        public async Task<int> CountLikesWhere(FilterSpecification<Like> filterSpecification = null)
        {
            return await _repository.CountWhereAsync(filterSpecification);
        }

        public async Task<GetLikeDto> GetLike(int id)
        {
            return _mapper.Map<GetLikeDto>(await _repository.GetAsync(id));
        }

        public async Task CheckLikeValidity(ILikeDto like)
        {
            if (await _userRepository.GetAsync(like.User) == null)
                throw new ResourceNotFoundException("User doesn't exist.");
            switch (like.LikeableType)
            {
                case LikeableType.Comment when await _commentRepository.GetAsync(like.Comment.Value) == null:
                    throw new ResourceNotFoundException("Comment doesn't exist.");
                case LikeableType.Post when await _postRepository.GetAsync(like.Post.Value) == null:
                    throw new ResourceNotFoundException("Post doesn't exist.");
            }
        }

        public async Task CheckLikeValidity(AddLikeDto like)
        {
            await CheckLikeValidity((ILikeDto)like);
            if (await _repository.LikeAlreadyExists(_mapper.Map<Like>(like)))
                throw new InvalidRequestException("Like already exists.");
        }

        public async Task<GetLikeDto> AddLike(AddLikeDto like)
        {
            await _dtoValidator.ValidateAndThrowAsync(like);
            await CheckLikeValidity(like);
            var result = await _repository.AddAsync(_mapper.Map<Like>(like));
            _unitOfWork.Save();
            return _mapper.Map<GetLikeDto>(result);
        }

        public async Task UpdateLike(UpdateLikeDto like)
        {
            await _dtoValidator.ValidateAndThrowAsync(like);
            if (await LikeAlreadyExistsWithSameProperties(like))
                return;
            await CheckLikeValidity(like);
            var likeEntity = await _repository.GetAsync(like.Id);
            _mapper.Map(like, likeEntity);
            _unitOfWork.Save();
        }

        public async Task DeleteLike(int id)
        {
            await _repository.RemoveAsync(await _repository.GetAsync(id));
            _unitOfWork.Save();
        }

        private async Task<bool> LikeAlreadyExistsWithSameProperties(UpdateLikeDto like)
        {
            var likeDb = await _repository.GetAsync(like.Id);
            return likeDb.Comment?.Id == like.Comment &&
                   likeDb.LikeableType == like.LikeableType &&
                   likeDb.Post?.Id == like.Post &&
                   likeDb.User.Id == like.User;
        }

        public async Task<IEnumerable<GetLikeDto>> GetLikesFromUser(int id)
        {
            return (await _repository.GetLikesFromUser(id)).Select(x => _mapper.Map<GetLikeDto>(x)).ToList();
        }

        public async Task<IEnumerable<GetLikeDto>> GetLikesFromPost(int id)
        {
            return (await _repository.GetLikesFromPost(id)).Select(x => _mapper.Map<GetLikeDto>(x)).ToList();
        }

        public async Task<IEnumerable<GetLikeDto>> GetLikesFromComment(int id)
        {
            return (await _repository.GetLikesFromComment(id)).Select(x => _mapper.Map<GetLikeDto>(x)).ToList();
        }
    }
}
